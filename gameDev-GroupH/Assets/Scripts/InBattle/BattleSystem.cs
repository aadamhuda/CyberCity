using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE}

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform[] enemyLocations;
    public Transform playerLocation;


    public BattleState state;

	public Text dialogue;



	public Enemy[] enemies;
    public Player players;

	public int target = 0;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(initialiseBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initialiseBattle()
    {
        GameObject playerObj =  Instantiate(playerPrefab, playerLocation);
        players = playerObj.GetComponent<Player>();
		enemies = new Enemy[3];

		for (int i = 0; i < enemyLocations.Length; i++)
        {
			Debug.Log(i);
			Debug.Log(enemyLocations.Length);
			Debug.Log(enemies.Length);


			GameObject enemyObj = Instantiate(enemyPrefab, enemyLocations[i]);
            enemies[i] = enemyObj.GetComponent<Enemy>();
        }

        state = BattleState.PLAYERTURN;

        yield return new WaitForSeconds(2f);

        playerTurn();
    }

	IEnumerator PlayerAttack()
	{
		bool enemiesDead = (enemies.Length == 0);
		
		bool isDead = enemies[target].takeDamage(players.damage);
		if (isDead)
        {
			removeEnemies(target);
			
        }

		dialogue.text = players.name + " attacked " + enemies[target].name;


		if (enemiesDead)
		{
			state = BattleState.WIN;
			yield return new WaitForSeconds(2f);
			endBattle();
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
			dialogue.text = enemies[i].name + " attacks!";
			yield return new WaitForSeconds(1f);

			isDead = players.takeDamage(enemies[i].damage);

		}


		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOSE;
			endBattle();
		}
		else
		{
			state = BattleState.PLAYERTURN;
			playerTurn();
		}

	}

	void endBattle()
	{
		if (state == BattleState.WIN)
		{
			dialogue.text = "You WIN the battle!";
		}
		else if (state == BattleState.LOSE)
		{
			dialogue.text = "You were defeated.";
		}
	}

	void playerTurn()
	{
		dialogue.text = "Choose an action:";
	}

	IEnumerator playerHeal()
	{
		players.heal(5);

		dialogue.text = "You healed by 5 hp!";

		state = BattleState.ENEMYTURN;
		yield return new WaitForSeconds(2f);
		StartCoroutine(EnemyTurn());
	}

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

	public void onChangeTargetButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		target = target + 1;

		if (target > enemies.Length - 1)
		{
			target = 0;
		}

		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies.Length > 0)
			{
				if(i == target)
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
