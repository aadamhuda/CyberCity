using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnController : MonoBehaviour
{
	private GameObject[] enemies; // Array of player and enemies turns
	private GameObject current;
	private int turnID;
	private GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindWithTag("Player");
		current = player;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

	public void doTurns()
	{

		for (int i = 0; i < enemies.Length; i++)
		{
			current = enemies[i];
			if (current.GetComponent<EnemyAttack>().isAlive)
			{
				enemies[i].GetComponent<EnemyAttack>().attack();

			}
            else
            {
				 enemies = removeEnemies(enemies, i);
            }


		}

		current = player;
		player.GetComponent<PlayerAttack>().canAttack = true;

	}

	public void endBattle()
    {

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private GameObject[] removeEnemies(GameObject[] enemies, int RemoveAt)
	{
		GameObject[] newEnemies = new GameObject[enemies.Length - 1];

		int i = 0;
		int j = 0;
		while (i < enemies.Length)
		{
			if (i != RemoveAt)
			{
				newEnemies[j] = enemies[i];
				j++;
			}

			i++;
		}

		return newEnemies;
	}

		void Update()
	{
		this.transform.position = current.transform.position;

		Vector3 temp = this.transform.position;
		temp.y += 2;
		transform.position = temp;

		if(enemies.Length == 0)
        {
			endBattle();
        }
	}
}
	