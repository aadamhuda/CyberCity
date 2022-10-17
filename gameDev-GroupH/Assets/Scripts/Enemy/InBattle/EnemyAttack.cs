using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

	public float hp;
	private float maxHP = 100;
	public bool isAlive;
	private GameObject player;
	private float enemyDamage = 20;


	// Start is called before the first frame update
	void Start()
	{
		hp = maxHP;
		isAlive = true;
		player = GameObject.FindWithTag("Player");
	}

	public void damage(float damageAmount)
	{
		hp -= damageAmount;
	}

	public void attack()
	{
		player.GetComponent<PlayerAttack>().damage(enemyDamage);
	}
}
