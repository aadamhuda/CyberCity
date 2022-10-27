using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

	public float hp;
	private float maxHP = 100;
	public bool isAlive;
	private GameObject player;
	private float enemyDamage = 15;


	// Start is called before the first frame update
	void Start()
	{
		hp = maxHP;
		isAlive = true;
		player = GameObject.FindWithTag("Player");
	}

	public void damage(float damageAmount)
	{
		//prevents negative hp, checks if it is less than 0
		if (hp - damageAmount < 1)
		{
			hp = 0;
			death(hp);
		}
		else
		{
			hp -= damageAmount;
		}

	}

	public void death(float hp)
	{
		if (hp == 0 & isAlive)
		{
			Debug.Log("enemy died");
			isAlive = false;

			disableEnemy();
		}
	}

	// function to enable
	public void enableEnemy()
	{
		gameObject.SetActive(true);
	}

	// function to disable
	public void disableEnemy()
	{
		gameObject.SetActive(false);
	}

	public void attack()
	{
		player.GetComponent<PlayerAttack>().damage(enemyDamage);
	}
}
