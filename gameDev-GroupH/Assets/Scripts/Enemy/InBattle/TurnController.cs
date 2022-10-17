using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	public void doTurns()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++)
		{
			current = enemies[i];
			if (current.GetComponent<EnemyAttack>().isAlive)
			{
				enemies[i].GetComponent<EnemyAttack>().attack();

			}

		}

		current = player;
		player.GetComponent<PlayerAttack>().canAttack = true;

	}

	void Update()
	{
		this.transform.position = current.transform.position;

		Vector3 temp = this.transform.position;
		temp.y += 2;
		transform.position = temp;
	}
}
	