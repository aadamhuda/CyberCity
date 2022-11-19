using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//NOTE: damage and healing must be balanced to provide a challenge while not making it too difficult

//enumerator to hold battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE}

public class BattleSystem : MonoBehaviour
{
	[SerializeField]
	SaveData savedata;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform[] enemyLocations;
	public Transform playerLocation; //to be changed to hold multiple players once party system is created

	public BattleState state;

	public TextMeshProUGUI dialogue;

	public Enemy[] enemies;
	public Player[] players;
	private int tracker = 0;
	public Player currPlayer;//this will change to an array once a party system is implemented

	//holds position of currently selected enemy
	public int target = 0;

	public GameObject restartButtonPrefab;
	public Transform rbLocation;

	public Button escapeButton;

	//HUD
	public GameObject hudPrefab;

	public UnitHUD[] playerHUD;
	public UnitHUD[] enemiesHUD;

	public RectTransform playerHudLocation;
	public RectTransform[] enemyHudLocations;

	private bool playerAttacking;
	private bool enemyAttacking;
	private float playerMinSpeed = 0.1f;
	private float playerMaxSpeed = 0.5f;

	// Start is called before the first frame update
	void Start()
    {
		//shows cursor so buttons can be selected
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		state = BattleState.START;
		playerAttacking = false;
		enemyAttacking = false;
        StartCoroutine(InitialiseBattle());
    }

	void Update()
	{
		//refreshes HUDs every frame
		for (int i = 0; i < playerHUD.Length; i++)
			playerHUD[i].updateHUD(players[i]);

		for (int i = 0; i < enemiesHUD.Length; i++)
		{
			enemiesHUD[i].updateHUD(enemies[i]);
		}
		//allows escape button to only be interacted with in player turn
		if (state != BattleState.PLAYERTURN)
        {
			escapeButton.interactable = false;
		}

        else
        {
			escapeButton.interactable = true;
		}

	}

	//spawns hud in placeholder regions
	void InitialiseHUD()
    {
		
        /*GameObject playerHudObj = Instantiate(hudPrefab, playerHudLocation);
        playerHUD = playerHudObj.GetComponent<UnitHUD>();*/

		//Multiple HUDS for each player

        playerHUD = new UnitHUD[4];

		for (int i = 0; i < players.Length; i++)
        {
			GameObject playerHudObj = Instantiate(hudPrefab, new Vector3(playerHudLocation.transform.position.x, playerHudLocation.transform.position.y - (i * 80f), playerHudLocation.transform.position.z), playerHudLocation.rotation, playerHudLocation);
			playerHUD[i] = playerHudObj.GetComponent<UnitHUD>();
		}

		playerHUD[tracker].GetComponent<Image>().color = Color.green; ;

		enemiesHUD = new UnitHUD[3];

		for (int i = 0; i < enemyHudLocations.Length; i++)
		{ 
			GameObject enemyHudObj = Instantiate(hudPrefab, enemyHudLocations[i]);
			enemiesHUD[i] = enemyHudObj.GetComponent<UnitHUD>();
		}
	}

    //initialises battle - spawns player and enemies, selects first target and then starts player turn
    IEnumerator InitialiseBattle()
    {
/*        GameObject playerObj =  Instantiate(playerPrefab, playerLocation);
        currPlayer = playerObj.GetComponent<Player>();

		currPlayer.unitName = "player";*/

		// Multiple Players

		players = new Player[4];

        for (int i = 0; i < players.Length; i++)
        {
            GameObject playerObj = Instantiate(playerPrefab, new Vector3(playerLocation.position.x + (i * 2.5f), playerLocation.position.y+1, playerLocation.position.z), playerLocation.rotation, playerLocation);
            players[i] = playerObj.GetComponent<Player>();
            players[i].unitName = "player" + (i + 1);
        }

		currPlayer = players[tracker];

        enemies = new Enemy[3];
		
		for (int i = 0; i < enemyLocations.Length; i++)
        {
			///Debug.Log(i);
			///Debug.Log(enemyLocations.Length);
			///Debug.Log(enemies.Length);
			
			GameObject enemyObj = Instantiate(enemyPrefab, enemyLocations[i]);
            enemies[i] = enemyObj.GetComponent<Enemy>();
			enemies[i].unitName = "enemy " + (i + 1);
        }

		ChangeTarget();

		InitialiseHUD();

		yield return new WaitForSeconds(1f);
		state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

	//applies damage to enemies and checks if they have won
	IEnumerator PlayerAttack()
	{
		Player playerScript = players[tracker].GetComponent<Player>();
		string currentAttack = playerScript.selectedMove;
		bool isDead = false;
		Debug.Log(currentAttack);

	
		// Coords of player start and enemy start positions
		Vector3 playerPos = currPlayer.transform.position;
		Vector3 enemyPos = enemies[target].transform.position;

		// Animator for player
		var animator = currPlayer.GetComponent<Animator>();

		// Player is attacking
		playerAttacking = true;

		// Rotating player until facing enemy
		yield return StartCoroutine(RotatePlayer(0.2f, enemyPos));
		yield return new WaitForSeconds(0.2f);


		if (currentAttack == "normal")
		{
			// Moving player until next to enemy
			yield return StartCoroutine(MovePlayer(true, playerMinSpeed, 2f, enemyPos));

			// Attack animation
			animator.CrossFade("Melee360High", 0.1f);
			yield return new WaitForSeconds(2.3f);

			isDead = enemies[target].takeDamage(((playerScript.playerAttacks["normal"])[0]));

			// Moving player back to original position
			yield return StartCoroutine(MovePlayer(false, playerMinSpeed, 0.1f, playerPos));
		}
		else if (currentAttack == "burn")
		{
			isDead = enemies[target].takeDamage(((playerScript.playerAttacks["burn"])[0]));
			enemies[target].burned = true;
			enemies[target].burnDamage = ((float)(playerScript.playerAttacks["burn"])[1] / 100);
			Debug.Log(enemies[target].burnDamage);
		}
		else if (currentAttack == "freeze")
		{
			enemies[target].frozen = true;
		}
		else if (currentAttack == "shoot")
        {
			for (int i = 0; i<enemies.Length; i++)
            {
				isDead = enemies[i].takeDamage(playerScript.playerAttacks["shoot"][0]);
				if (isDead && enemies[i] != enemies[target])
                {
					enemies = RemoveEnemies(i);
					enemiesHUD = RemoveHUDs(i, enemiesHUD);
				}
            }
        }

		dialogue.text = currPlayer.unitName + " attacked " + enemies[target].unitName;



		if (isDead)
        {
			enemies = RemoveEnemies(target);
			enemiesHUD = RemoveHUDs(target, enemiesHUD);
			ChangeTarget(); //automatically changes target on enemy death
        }

		//checks if all enemies are dead - win condition
		bool enemiesDead = (enemies.Length == 0);

		if (enemiesDead)
		{
			state = BattleState.WIN;
			yield return new WaitForSeconds(2f);
			StartCoroutine(EndBattle());
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
			Increment(1);
				
		}

		// Player attack is finished
		playerAttacking = false;
	}

    IEnumerator EnemyTurn()
	{
		bool isDead = false;

		Debug.Log(128937218973);

		for (int i = 0; i < enemies.Length; i++)
		{
			int player_target = Random.Range(0, players.Length);

			dialogue.text = enemies[i].unitName + " attacks!";
			yield return new WaitForSeconds(1f);

			if (enemies[i].frozen)
            {
				// skip turn
				int number = UnityEngine.Random.Range(0, 100);
				// 34% chance to unfreeze 
				if (number > 66)
				{
					enemies[i].frozen = false;
				}
			}
            else
            {
				//adds 15% damage if enemy hits player first
				if (savedata.EnemyDouble == true)
					isDead = players[player_target].takeDamage((float)(enemies[i].damage * 1.15));
				else //regular damage
				{
					isDead = players[player_target].takeDamage(enemies[i].damage);
				}
			}

			//deal burn damage
			if (enemies[i].burned)
            {
				float damage = (float)enemies[i].maxHP * enemies[i].burnDamage * enemies[i].burnMultiplier;
				Debug.Log(damage);
				Debug.Log("damage");
				isDead = enemies[i].takeDamage(damage);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop burning 
				if (number > 80)
				{
					enemies[i].burned = false;
				}
			}
			//stops enemy attacking if player is already dead

			if (isDead)
            {
				players = RemovePlayer(player_target);
				playerHUD = RemoveHUDs(player_target, playerHUD);

            }
			//playerHUD.updateHUD(currPlayer);
		}

		bool playersDeath = (players.Length == 0);

		yield return new WaitForSeconds(1f);

		if (playersDeath)
		{
			state = BattleState.LOSE;
			StartCoroutine(EndBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			Increment(0);
			PlayerTurn();
		}

	}

	//initialises restart button - ran after state = LOSE
	public void createRestartButton()
    {
		GameObject rb = Instantiate(restartButtonPrefab, rbLocation);
		Button restartButton = rb.GetComponent<Button>();
		restartButton.onClick.AddListener(OnRestartButton);

	}

	IEnumerator EndBattle()
	{
		if (state == BattleState.WIN)
		{
			dialogue.text = "You WIN the battle!";
			savedata.DictBoolSwitch(savedata.Death, savedata.GetEnemy());
			savedata.OffEnemyDouble();
			yield return new WaitForSeconds(3f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		else if (state == BattleState.LOSE)
		{
			dialogue.text = "You were defeated.";
			yield return new WaitForSeconds(2f);

			createRestartButton();
		}
	}

	void PlayerTurn()
	{
		dialogue.text = "Choose an action!";
	}

	IEnumerator PlayerHeal()
	{

		int amount = Random.Range(60, 100);
		currPlayer.heal(amount);

		dialogue.text = "You healed by " +amount+ " hp!";

		//playerHUD.updateHUD(currPlayer);

		yield return new WaitForSeconds(2f);
		Increment(1);
	}

	private Player [] RemovePlayer(int removeAt)
    {
		Player[] new_players = new Player[players.Length - 1];

		//disables the enemy object that has died
		players[removeAt].disableEnemy();

		int i = 0;
		int j = 0;
		while (i < players.Length)
		{
			if (i != removeAt)
			{
				new_players[j] = players[i];
				j++;
			}

			i++;
		}
		return new_players;
	} 

	//removes enemy from array, disables gameObject and returns new array - to be used on enemy death
	private Enemy[] RemoveEnemies(int removeAt)
	{
		Enemy[] new_enemies = new Enemy[enemies.Length - 1];

		//disables the enemy object that has died
		enemies[removeAt].disableEnemy();

		int i = 0;
		int j = 0;
		while (i < enemies.Length)
		{
			if (i != removeAt)
			{
				new_enemies[j] = enemies[i];
				j++;
			}

			i++;
		}
		return new_enemies;
	}

	private UnitHUD[] RemoveHUDs(int removeAt, UnitHUD [] arr)
	{
		UnitHUD[] newHUD = new UnitHUD[arr.Length - 1];

		//disables the Objects object that has died
		arr[removeAt].disableHUD();

		int i = 0;
		int j = 0;
		while (i < arr.Length)
		{
			if (i != removeAt)
			{
				newHUD[j] = arr[i];
				j++;
			}

			i++;
		}
		return newHUD;
	}


	//allows player to change targeted enemy
	public void ChangeTarget()
    {
		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies.Length > 0)
			{
				if (i == target)
				{
					enemies[i].GetComponent<Renderer>().material.color = Color.blue;
				}
				else
				{
					enemies[i].GetComponent<Renderer>().material.color = Color.red;
				}
			}
		}
	}

	private void Increment(int incre)
    {
		// Iterates to next player switching turns when all of currPlayer have had a turn

		Debug.Log(tracker + " : " +  incre + " : " + players.Length + " : " + (tracker % players.Length));
		playerHUD[tracker].GetComponent<Image>().color = Color.white;
		tracker += incre;
		Debug.Log(tracker + " : " + incre + " : " + players.Length + " : " + (tracker % players.Length));
		if (tracker == players.Length)
		{
			tracker = 0;
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
		else
        {
			playerHUD[tracker].GetComponent<Image>().color = Color.green;

			currPlayer = players[tracker];

			PlayerTurn();
		}

	}

	// Turn player to a position
	IEnumerator RotatePlayer(float speed, Vector3 targetPos)
	{
		var transform = currPlayer.transform;
		var startRotation = transform.rotation;
		var direction = targetPos - transform.position;
		var targetRotation = Quaternion.LookRotation(direction);
		targetRotation.x = 0;
		var t = 0f;
		while (t <= 1f)
		{
			t += Time.deltaTime / speed;
			transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
			yield return null;
		}
		transform.rotation = targetRotation;
	}

	// Move player to a position
	IEnumerator MovePlayer(bool forward, float speed, float distance, Vector3 targetPos)
	{
		var transform = currPlayer.transform;
		var cc = currPlayer.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = currPlayer.GetComponent<Animator>();
		animator.SetBool("moveBackwards", !forward);
		// Gradually speed up until threshold distance reached
		while (Vector3.Distance(transform.position, targetPos) > distance*1.5)
		{
			if (speed > playerMaxSpeed)
            {
				speed = playerMaxSpeed;
            }
			animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.01f;
			yield return null;
		}

		// Gradually slow down until final distance reached
		while (Vector3.Distance(transform.position, targetPos) > distance)
		{

			if (speed < playerMinSpeed)
			{
				speed = playerMinSpeed;
			}
			animator.SetFloat("Speed", speed);
			cc.Move(offset * speed * Time.deltaTime);
			speed -= 0.01f;
			yield return null;
		}
		// Reached specified distance from target, stop moving
		animator.SetFloat("Speed", 0f, 0f, Time.deltaTime);
		playerMinSpeed = 0.1f;

		// Moving forwards by default
		animator.SetBool("moveBackwards", true);
	}

	//reloads scene on restart
	IEnumerator Restart()
    {
		dialogue.text = "Restarting Battle...";
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}

	//sets save data to spawn coordinates to allow player to escape to spawn
	IEnumerator EscapeToSpawn()
	{
		dialogue.text = "Escaping Battle...";
		yield return new WaitForSeconds(2f);

		savedata.SaveLocation((float)-115.4, 1, (float)-65.9);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}


	//button methods
	public void OnChangeTargetButton()
	{
		if (state != BattleState.PLAYERTURN || playerAttacking)
			return;

		target = target + 1;
		if (target > enemies.Length - 1)
		{
			target = 0;
		}
		StartCoroutine(RotatePlayer(0.2f, enemies[target].transform.position));
		ChangeTarget();
	}

	public void OnChangeAttackButton()
    {
		if (state != BattleState.PLAYERTURN)
			return;
		currPlayer.GetComponent<Player>().changeAttack();
    }

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN || playerAttacking)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN || playerAttacking)
			return;

		StartCoroutine(PlayerHeal());
	}

	public void OnRestartButton()
	{
		if (state != BattleState.LOSE)
			return;

		StartCoroutine(Restart());
	}

    public void OnEscapeButton()
    {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(EscapeToSpawn());
	}
}
