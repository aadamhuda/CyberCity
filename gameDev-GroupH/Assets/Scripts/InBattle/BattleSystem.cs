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

	public UnitHUD playerHUD;
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
		playerHUD.updateHUD(players);

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
		GameObject playerHudObj = Instantiate(hudPrefab, playerHudLocation);
		playerHUD = playerHudObj.GetComponent<UnitHUD>();

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


		playerers = new Player[4];

        for (int i = 0; i < playerers.Length; i++)
        {
            GameObject playerObj = Instantiate(playerPrefab, new Vector3(playerLocation.position.x + (i * 2.5f), playerLocation.position.y+1, playerLocation.position.z), playerLocation.rotation);
            playerObj.transform.localScale = new Vector3(1, 1, 1);
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
		bool isDead = enemies[target].takeDamage(players.damage);

		dialogue.text = players.unitName + " attacked " + enemies[target].unitName;

		if (isDead)
        {
			enemies = RemoveEnemies(target);
			enemiesHUD = RemoveHUDs(target);
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
			Debug.Log("I was Here");
			
			yield return new WaitForSeconds(0.5f);
			tracker++;
			Debug.Log(tracker);
			players = playerers[tracker % 4];
			if (tracker % 4 == 0)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyTurn());
			}
			else
				PlayerTurn();
				
		}
	}

	IEnumerator EnemyTurn()
	{
		bool isDead = false;

		Debug.Log(128937218973);

		for (int i = 0; i < enemies.Length; i++)
		{
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
					isDead = players.takeDamage((float)(enemies[i].damage * 1.15));
				else //regular damage
				{
					isDead = players.takeDamage(enemies[i].damage);
				}
			}

			//deal burn damage
			if (enemies[i].burned)
            {
				float damage = enemies[i].maxHP * enemies[i].burnDamage * enemies[i].burnMultiplier;
				enemies[i].takeDamage(damage);
				int number = UnityEngine.Random.Range(0, 100);
				// 20% chance to stop burning 
				if (number > 80)
				{
					enemies[i].burned = false;
				}
			}
			//stops enemy attacking if player is already dead

			if (isDead)
				break;
			//playerHUD.updateHUD(players);
		}

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOSE;
			StartCoroutine(EndBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
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

		state = BattleState.ENEMYTURN;
		yield return new WaitForSeconds(2f);
		tracker++;
		players = playerers[tracker % 4];
		if (tracker % 4 == 0)
			StartCoroutine(EnemyTurn());
	}

	//removes enemy from array, disables gameObject and returns new array - to be used on enemy death
	private Enemy[] RemoveEnemies(int removeAt)
	{
		Enemy[] newEnemies = new Enemy[enemies.Length - 1];

		//disables the enemy object that has died
		enemies[removeAt].disableEnemy();

		int i = 0;
		int j = 0;
		while (i < enemies.Length)
		{
			if (i != removeAt)
			{
				newEnemies[j] = enemies[i];
				j++;
			}

			i++;
		}
		return newEnemies;
	}

	private UnitHUD[] RemoveHUDs(int removeAt)
	{
		UnitHUD[] newEnemiesHUD = new UnitHUD[enemiesHUD.Length - 1];

		//disables the enemy object that has died
		enemiesHUD[removeAt].disableHUD();

		int i = 0;
		int j = 0;
		while (i < enemiesHUD.Length)
		{
			if (i != removeAt)
			{
				newEnemiesHUD[j] = enemiesHUD[i];
				j++;
			}

			i++;
		}
		return newEnemiesHUD;
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
