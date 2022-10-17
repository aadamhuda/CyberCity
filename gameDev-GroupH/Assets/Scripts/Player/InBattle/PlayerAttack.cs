using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public int playerDamage = 20; // Damage player deals to enemy

	private GameObject target;
	private int targetID = 0;
	public bool canAttack;
	public bool isAlive;
	public float hp;
	private float maxHP = 100;
	private float resist;


	void Start()
	{
		canAttack = true;
		isAlive = true;
		resist = 1;
		findTargets();
		hp = maxHP;
	}

	void findTargets()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		target = targets[targetID]; // Default Target
		target.GetComponent<Renderer>().material.color = Color.blue;
	}

	public void changeTarget()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < targets.Length; i++)
		{
			target.GetComponent<Renderer>().material.color = Color.red;
		}

		targetID = targetID + 1;

		if (targetID > targets.Length - 1)
		{
			targetID = 0;
		}

		target = targets[targetID];
		target.GetComponent<Renderer>().material.color = Color.blue;
	}

	public void doAttack()
	{
		if (canAttack)
		{
			EnemyAttack enemyScript = target.GetComponent<EnemyAttack>();
			enemyScript.damage(playerDamage);

			// Enemies turn
			canAttack = false;
			GameObject.FindWithTag("TurnController").GetComponent<TurnController>().doTurns();
		}

	}

	public void doGuard()
	{
		if (canAttack)
		{
			float prevResist = resist;
			resist = resist / 2;

			// Enemies turn
			canAttack = false;
			GameObject.FindWithTag("TurnController").GetComponent<TurnController>().doTurns();
			resist = prevResist;
		}
	}

	public void damage(float damageAmount)
	{
		hp -= damageAmount * resist;
	}

}
