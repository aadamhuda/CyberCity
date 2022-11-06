using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	public Player players;//this will change to an array once a party system is implemented

	//holds position of currently selected enemy
	public int target = 0;


	//HUD
	public GameObject hudPrefab;

	public UnitHUD playerHUD;
	public UnitHUD[] enemiesHUD;

	public RectTransform playerHudLocation;
	public RectTransform[] enemyHudLocations;

	// Start is called before the first frame update
	void Start()
    {
        state = BattleState.START;
        StartCoroutine(InitialiseBattle());
    }

	void Update()
	{
		playerHUD.updateHUD(players);

		for (int i = 0; i < enemiesHUD.Length; i++)
		{
			enemiesHUD[i].updateHUD(enemies[i]);
		}

		if (Input.GetKeyUp(KeyCode.Escape)) {
			savedata.SaveLocation((float)-115.4, 1, (float)-65.9);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}


	void initialiseHUD()
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
        GameObject playerObj =  Instantiate(playerPrefab, playerLocation);
        players = playerObj.GetComponent<Player>();

		players.unitName = "player";

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

		changeTarget();

		initialiseHUD();
		//playerHUD.updateHUD(players);
		
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(2f);
        playerTurn();
    }

	IEnumerator PlayerAttack()
	{	
		bool isDead = enemies[target].takeDamage(players.damage);

		dialogue.text = players.unitName + " attacked " + enemies[target].unitName;

		if (isDead)
        {
			enemies = removeEnemies(target);
			enemiesHUD = removeHUDs(target);
			changeTarget(); //automatically changes target on enemy death
        }

		//checks if all enemies are dead - win condition
		bool enemiesDead = (enemies.Length == 0);

		if (enemiesDead)
		{
			state = BattleState.WIN;
			yield return new WaitForSeconds(2f);
			StartCoroutine(endBattle());
		}
		else
		{
			state = BattleState.ENEMYTURN;
			yield return new WaitForSeconds(2f);
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		bool isDead = false;

		for (int i = 0; i < enemies.Length; i++)
		{
			dialogue.text = enemies[i].unitName + " attacks!";
			yield return new WaitForSeconds(1f);

			isDead = players.takeDamage(enemies[i].damage);
			if (savedata.EnemyDouble == true)
				isDead = players.takeDamage(enemies[i].damage/enemies.Length);
			//playerHUD.updateHUD(players);
		}

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOSE;
			StartCoroutine(endBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			playerTurn();
		}

	}

	IEnumerator endBattle()
	{
		if (state == BattleState.WIN)
		{
			dialogue.text = "You WIN the battle!";
			savedata.killEnem(savedata.Death, savedata.GetEnemy());
			savedata.OffEnemyDouble();
			yield return new WaitForSeconds(3f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		else if (state == BattleState.LOSE)
		{
			dialogue.text = "You were defeated.";
			yield return new WaitForSeconds(3f);
		}
	}

	void playerTurn()
	{
		dialogue.text = "Choose an action!";
	}

	//NOTE: requires balancing, healing = 100 for testing purposes
	IEnumerator playerHeal()
	{

		int amount = Random.Range(60, 100);
		players.heal(amount);

		dialogue.text = "You healed by " +amount+ " hp!";

		//playerHUD.updateHUD(players);

		state = BattleState.ENEMYTURN;
		yield return new WaitForSeconds(2f);
		StartCoroutine(EnemyTurn());
	}

	//removes enemy from array, disables gameObject and returns new array - to be used on enemy death
	private Enemy[] removeEnemies(int removeAt)
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

	private UnitHUD[] removeHUDs(int removeAt)
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



	public void changeTarget()
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
	public void onChangeTargetButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		target = target + 1;
		if (target > enemies.Length - 1)
		{
			target = 0;
		}

		changeTarget();
	}

	public void onAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void onHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(playerHeal());
	}
}
