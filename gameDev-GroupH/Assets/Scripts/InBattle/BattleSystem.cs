using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//NOTE: damage and healing must be balanced to provide a challenge while not making it too difficult

//enumerator to hold battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE, PLAYERWAIT}

public class BattleSystem : MonoBehaviour
{
	[SerializeField]
	SaveData savedata;

	public GameObject playerPrefab;
	public GameObject companion1Prefab;
	public GameObject companion2Prefab;
	public GameObject companion3Prefab;

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

	public RectTransform[] playerHudLocations;
	public RectTransform[] enemyHudLocations;

	private bool playerAttacking;
	private bool enemyAttacking;
	private float playerMinSpeed = 0.1f;
	private float playerMaxSpeed = 0.5f;

	// Start is called before the first frame update
	void Start()
    {
		//saves that the player is in battle
		savedata.inBattle = true;

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
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

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
        playerHUD = new UnitHUD[4];

		for (int i = 0; i < players.Length; i++)
        {
			GameObject playerHudObj = Instantiate(hudPrefab, playerHudLocations[i]);
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

	public Player[] InstantiatePlayers()
    {
		Player[] allPlayers = new Player[4];

		//main player

		GameObject playerObj = Instantiate(playerPrefab, new Vector3(playerLocation.position.x + (0 * 2.5f), playerLocation.position.y + 1, playerLocation.position.z), playerLocation.rotation, playerLocation);
		allPlayers[0] = playerObj.GetComponent<Player>();
		allPlayers[0].unitName = "Main Character";
		allPlayers[0].setHealth(savedata.team_health[0]);

		//Companion 1
		playerObj = Instantiate(companion1Prefab, new Vector3(playerLocation.position.x + (1 * 2.5f), playerLocation.position.y + 1, playerLocation.position.z), playerLocation.rotation, playerLocation);
		allPlayers[1] = playerObj.GetComponent<Player>();
		allPlayers[1].unitName = "Companion_1 ";
		allPlayers[1].setHealth(savedata.team_health[1]);

		//Companion 2
		playerObj = Instantiate(companion2Prefab, new Vector3(playerLocation.position.x + (2 * 2.5f), playerLocation.position.y + 1, playerLocation.position.z), playerLocation.rotation, playerLocation);
		allPlayers[2] = playerObj.GetComponent<Player>();
		allPlayers[2].unitName = "Companion_2";
		allPlayers[2].setHealth(savedata.team_health[2]);

		//Companion 3
		playerObj = Instantiate(companion3Prefab, new Vector3(playerLocation.position.x + (3 * 2.5f), playerLocation.position.y + 1, playerLocation.position.z), playerLocation.rotation, playerLocation);
		allPlayers[3] = playerObj.GetComponent<Player>();
		allPlayers[3].unitName = "Companion_3";
		allPlayers[3].setHealth(savedata.team_health[3]);

		return allPlayers;
	}

    //initialises battle - spawns player and enemies, selects first target and then starts player turn
    IEnumerator InitialiseBattle()
    {
		players = InstantiatePlayers();
		// player moves
		players[0].playerAttacks.Add("normal", new int[] { 0, 20 }); // type, damage
		players[0].playerAttacks.Add("curse", new int[] { -1 }); // no type
		players[0].playerAttacks.Add("shoot", new int[] { 0, 6, 15 }); // type, damage, side damage
		players[0].selectedMove = "normal";

		players[1].playerAttacks.Add("normal", new int[] { 0, 20 }); 
		players[1].playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
		players[1].playerAttacks.Add("fire", new int[] { 1, 15, 0 }); 
		players[1].selectedMove = "normal";

		players[2].playerAttacks.Add("normal", new int[] { 0, 20 }); 
		players[2].playerAttacks.Add("grass", new int[] { 2, 15, 0 }); 
		players[2].playerAttacks.Add("poison", new int[] { -1, 10 }); 
		players[2].selectedMove = "normal";

		players[3].playerAttacks.Add("normal", new int[] { 0, 20 }); 
		players[3].playerAttacks.Add("water", new int[] { 3, 15, 0 }); 
		players[3].playerAttacks.Add("freeze", new int[] { -1 }); 
		players[3].selectedMove = "normal";

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
		state = BattleState.PLAYERWAIT;
		playerAttacking = true;

		// Rotating player until facing enemy
		yield return StartCoroutine(RotatePlayer(currPlayer, 0.2f, enemyPos));
		yield return new WaitForSeconds(0.2f);
	
		if (currentAttack == "burn")
		{
			isDead = enemies[target].takeDamage(((playerScript.playerAttacks[currentAttack])[1]), ((playerScript.playerAttacks[currentAttack])[0]));
			enemies[target].burned = true;
			enemies[target].burnDamage = ((float)(playerScript.playerAttacks["burn"])[2] / 100);
		}
		else if (currentAttack == "poison")
		{
			enemies[target].poisoned = true;
			enemies[target].poisonDamage = ((float)(playerScript.playerAttacks["poison"])[1] / 100);
		}
		else if (currentAttack == "curse")
        {
			enemies[target].cursed = true;
        }
		else if (currentAttack == "freeze")
		{
			enemies[target].frozen = true;
		}
		else if (currentAttack == "shoot")
        {
			for (int i = 0; i<enemies.Length; i++)
            {
				var isThisDead = enemies[i].takeDamage(((playerScript.playerAttacks[currentAttack])[1]), ((playerScript.playerAttacks[currentAttack])[0]));
				if (isThisDead)
                {
					enemies = RemoveEnemies(i);
					enemiesHUD = RemoveHUDs(i, enemiesHUD);
				}
            }
        }
        else 
        {
			// Moving player until next to enemy
			yield return StartCoroutine(MovePlayer(currPlayer, true, playerMinSpeed, 2f, enemyPos));

			// Attack animation
			animator.CrossFade("Melee360High", 0.1f);
			yield return new WaitForSeconds(2.3f);

			isDead = enemies[target].takeDamage(((playerScript.playerAttacks[currentAttack])[1]), ((playerScript.playerAttacks[currentAttack])[0]));

			// Moving player back to original position
			yield return StartCoroutine(MovePlayer(currPlayer, false, playerMinSpeed, 0.1f, playerPos));
		}

		dialogue.text = currPlayer.unitName + " attacked " + enemies[target].unitName;
		yield return new WaitForSeconds(2f);



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
		state = BattleState.PLAYERTURN;
		playerAttacking = false;
	}
	void PlayerTurn()
	{
		dialogue.text = "Choose an action!";
		TextMeshProUGUI indicator = GameObject.FindWithTag("attackIndicator").GetComponent<TextMeshProUGUI>();
		indicator.text = players[tracker].selectedMove;
	}

	IEnumerator PlayerHeal()
	{

		int amount = Random.Range(60, 100);
		currPlayer.heal(amount);

		dialogue.text = "You healed by " + amount + " hp!";

		//playerHUD.updateHUD(currPlayer);
		state = BattleState.PLAYERWAIT;
		yield return new WaitForSeconds(2f);
		Increment(1);
	}
//-------------------------------------------ENEMY-------------------------------------------------------
	IEnumerator EnemyTurn()
	{
		bool isDead = false;

		for (int i = 0; i < enemies.Length; i++)
		{

			int player_target = Random.Range(0, players.Length);

			// Current enemy
			Enemy currEnemy = enemies[i];

			// Coords of player start and enemy start positions
			Vector3 playerPos = players[player_target].transform.position;
			Vector3 enemyPos = currEnemy.transform.position;

			// Animator for current enemy
			var animator = currEnemy.GetComponent<Animator>();

			dialogue.text = enemies[i].unitName + " attacks!";
			yield return new WaitForSeconds(1f);

			// Rotating enemy until facing player
			yield return StartCoroutine(RotateEnemy(currEnemy, 0.2f, playerPos));
			yield return new WaitForSeconds(0.2f);

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
				// Moving enemy until next to player
				yield return StartCoroutine(MoveEnemy(currEnemy, true, playerMinSpeed, 2f, playerPos));

				// Attack animation
				animator.CrossFade("Melee360High", 0.1f);
				yield return new WaitForSeconds(2.3f);

				//adds 15% damage if enemy hits player first
				if (savedata.EnemyDouble == true)
					isDead = players[player_target].takeDamage((float)(enemies[i].damage * 1.15), 1); // change 1 to enemy's move type
				else //regular damage
				{
					isDead = players[player_target].takeDamage(enemies[i].damage, 1);
				}
				// Moving enemy back to original position
				yield return StartCoroutine(MoveEnemy(currEnemy, false, playerMinSpeed, 0.1f, enemyPos));
			}

			//deal burn damage
			if (enemies[i].burned)
            {
				float damage = (float)enemies[i].maxHP * enemies[i].burnDamage * enemies[i].burnMultiplier;
				isDead = enemies[i].takeDamage(damage, 1);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop burning 
				if (number > 80)
				{
					enemies[i].burned = false;
				}
			}

			//deal poison damage
			if (enemies[i].poisoned)
			{
				isDead = enemies[i].takeDamage(enemies[i].poisonDamage, -1);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop poisoned
				if (number > 80)
				{
					enemies[i].poisoned = false;
				}
			}

			//stops enemy attacking if player is already dead

			if (isDead)
            {
				players = RemovePlayer(player_target);
				playerHUD = RemoveHUDs(player_target, playerHUD);

            }
			//playerHUD.updateHUD(currPlayer);

			if (players.Length == 0)
				break;
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

	//-------------------------------------------BATTLE LOOP-------------------------------------------------------
	private void Increment(int incre)
	{
		// Iterates to next player switching turns when all of currPlayer have had a turn

		playerHUD[tracker].GetComponent<Image>().color = Color.white;
		tracker += incre;
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
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	IEnumerator EndBattle()
	{
		if (state == BattleState.WIN)
		{
			dialogue.text = "You WIN the battle!";
			savedata.DictBoolSwitch(savedata.Death, savedata.GetEnemy());
			savedata.OffEnemyDouble();
			savedata.SavePlayerHealth(new float[] { players[0].currentHP, players[1].currentHP,  players[2].currentHP, players[3].currentHP });
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

//-------------------------------------------ARRAY FUNCTIONS-------------------------------------------------------

	//note - will need changing for revives - players must not be deactivated but rather have a downed state
	private Player [] RemovePlayer(int removeAt)
    {
		Player[] new_players = new Player[players.Length - 1];

		//disables the enemy object that has died
		players[removeAt].disableUnit();

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
		enemies[removeAt].disableUnit();

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



//-------------------------------------------ANIMATIONS/MOVEMENT-------------------------------------------------------

	// Turn player to a position
	IEnumerator RotatePlayer(Player p,  float speed, Vector3 targetPos)
	{
		var transform = p.transform;
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

	// Turn enemy to a position
	IEnumerator RotateEnemy(Enemy e, float speed, Vector3 targetPos)
	{
		var transform = e.transform;
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
	IEnumerator MovePlayer(Player p, bool forward, float speed, float distOffsetToTarget, Vector3 targetPos)
	{
		var transform = p.transform;
		var cc = p.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = p.GetComponent<Animator>();
		var slowDown = 0.6f;
		if(!forward)
        {
			slowDown = 0.3f;
        }
		// Movement
		animator.SetBool("moveBackwards", !forward);
		animator.SetBool("isMoving", true);

		// Gradually speed up until close to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget + slowDown)
		{
			if (speed > playerMaxSpeed)
			{
				speed = playerMaxSpeed;
			}

			animator.SetFloat("Speed", speed, 0f, Time.deltaTime);;
			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.01f;
			yield return null;
		}

			animator.SetBool("isMoving", false);

		// Slow down until next to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget)
		{
			if (speed < playerMinSpeed)
			{
				speed = playerMinSpeed;
			}

			animator.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
			cc.Move(offset * speed * Time.deltaTime);
			speed -= 0.01f;

			yield return null;
		}

		animator.SetBool("isMoving", false);
	}

	// Move enemy to a position
	IEnumerator MoveEnemy(Enemy e, bool forward, float speed, float distOffsetToTarget, Vector3 targetPos)
	{
		var transform = e.transform;
		var cc = e.GetComponent<CharacterController>();
		var offset = targetPos - transform.position;
		var animator = e.GetComponent<Animator>();
		var slowDown = 0.6f;
		if (!forward)
		{
			slowDown = 0.3f;
		}
		// Movement
		animator.SetBool("moveBackwards", !forward);
		animator.SetBool("isMoving", true);

		// Gradually speed up until close to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget + slowDown)
		{
			if (speed > playerMaxSpeed)
			{
				speed = playerMaxSpeed;
			}

			animator.SetFloat("Speed", speed, 0f, Time.deltaTime); ;
			cc.Move(offset * speed * Time.deltaTime);
			speed += 0.01f;
			yield return null;
		}

		animator.SetBool("isMoving", false);

		// Slow down until next to target
		while (Vector3.Distance(transform.position, targetPos) > distOffsetToTarget)
		{
			if (speed < playerMinSpeed)
			{
				speed = playerMinSpeed;
			}

			animator.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
			cc.Move(offset * speed * Time.deltaTime);
			speed -= 0.01f;

			yield return null;
		}

		animator.SetBool("isMoving", false);
	}

//-------------------------------------------BUTTONS-------------------------------------------------------

	//initialises restart button - ran after state = LOSE
	public void createRestartButton()
	{
		GameObject rb = Instantiate(restartButtonPrefab, rbLocation);
		Button restartButton = rb.GetComponent<Button>();
		restartButton.onClick.AddListener(OnRestartButton);

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
		if (state != BattleState.PLAYERTURN)
			return;

		target = target + 1;
		if (target > enemies.Length - 1)
		{
			target = 0;
		}
		StartCoroutine(RotatePlayer(currPlayer, 0.2f, enemies[target].transform.position));
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
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
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
