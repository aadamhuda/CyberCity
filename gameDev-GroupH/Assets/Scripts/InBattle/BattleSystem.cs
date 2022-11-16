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
	public Player[] playerers;
	private int tracker = 0;
	public Player players;//this will change to an array once a party system is implemented

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

	// Start is called before the first frame update
	void Start()
    {
		//shows cursor so buttons can be selected
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		state = BattleState.START;
        StartCoroutine(InitialiseBattle());
    }

	void Update()
	{
		//refreshes HUDs every frame
		for (int i = 0; i < playerHUD.Length; i++)
			playerHUD[i].updateHUD(playerers[i]);

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

		for (int i = 0; i < playerers.Length; i++)
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
        players = playerObj.GetComponent<Player>();

		players.unitName = "player";*/

		// Multiple Players

		playerers = new Player[4];

        for (int i = 0; i < playerers.Length; i++)
        {
            GameObject playerObj = Instantiate(playerPrefab, new Vector3(playerLocation.position.x + (i * 2.5f), playerLocation.position.y+1, playerLocation.position.z), playerLocation.rotation, playerLocation);
            playerers[i] = playerObj.GetComponent<Player>();
            playerers[i].unitName = "player" + (i + 1);
        }

		players = playerers[tracker];

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
		
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

	//applies damage to enemies and checks if they have won
	IEnumerator PlayerAttack()
	{
		Player playerScript = playerers[tracker].GetComponent<Player>();
		string currentAttack = playerScript.selectedMove;
		bool isDead = false;
		Debug.Log(currentAttack);
		if (currentAttack == "normal")
		{
			isDead = enemies[target].takeDamage(((playerScript.playerAttacks["normal"])[0]));
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

		dialogue.text = players.unitName + " attacked " + enemies[target].unitName;

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
	}

	IEnumerator EnemyTurn()
	{
		bool isDead = false;

		Debug.Log(128937218973);

		for (int i = 0; i < enemies.Length; i++)
		{
			int player_target = Random.Range(0, playerers.Length);

			dialogue.text = enemies[i].unitName + " attacks!";
			yield return new WaitForSeconds(1f);

			if (!enemies[i].frozen)
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
					isDead = playerers[player_target].takeDamage((float)(enemies[i].damage * 1.15));
				else //regular damage
				{
					isDead = playerers[player_target].takeDamage(enemies[i].damage);
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
				playerers = RemovePlayer(player_target);
				playerHUD = RemoveHUDs(player_target, playerHUD);

            }
			//playerHUD.updateHUD(players);
		}

		bool playerersDeath = (playerers.Length == 0);

		yield return new WaitForSeconds(1f);

		if (playerersDeath)
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
		players.heal(amount);

		dialogue.text = "You healed by " +amount+ " hp!";

		//playerHUD.updateHUD(players);

		yield return new WaitForSeconds(2f);
		Increment(1);
	}

	private Player [] RemovePlayer(int removeAt)
    {
		Player[] new_playerers = new Player[playerers.Length - 1];

		//disables the enemy object that has died
		playerers[removeAt].disableEnemy();

		int i = 0;
		int j = 0;
		while (i < playerers.Length)
		{
			if (i != removeAt)
			{
				new_playerers[j] = playerers[i];
				j++;
			}

			i++;
		}
		return new_playerers;
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
		// Iterates to next player switching turns when all of players have had a turn

		Debug.Log(tracker + " : " +  incre + " : " + playerers.Length + " : " + (tracker % playerers.Length));
		playerHUD[tracker].GetComponent<Image>().color = Color.white;
		tracker += incre;
		Debug.Log(tracker + " : " + incre + " : " + playerers.Length + " : " + (tracker % playerers.Length));
		if (tracker == playerers.Length)
		{
			tracker = 0;
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
		else
        {
			playerHUD[tracker].GetComponent<Image>().color = Color.green;

			players = playerers[tracker];

			PlayerTurn();
		}

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

	
	public void OnChangeTargetButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		target = target + 1;
		if (target > enemies.Length - 1)
		{
			target = 0;
		}

		ChangeTarget();
	}

	public void OnChangeAttackButton()
    {
		if (state != BattleState.PLAYERTURN)
			return;
		players.GetComponent<Player>().changeAttack();
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
