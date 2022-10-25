using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

	private GameObject player;
	private GameObject restartButton;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		restartButton = GameObject.FindGameObjectWithTag("Restart");
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
		//prevents negative hp, checks if it is less than 0
		if(hp - (damageAmount * resist) < 0)
        {
			hp = 0;
			death(hp);
        }
        else
        {
			hp -= damageAmount * resist;
		}
		
	}


	public void death(float hp)
    {
		if (hp == 0 & isAlive)
        {
			Debug.Log("you have died");
			isAlive = false;
			disablePlayer(player);

			restartButton.GetComponent<RestartButton>().enableButton(restartButton);

		}
    }

	// function to enable
	public void enablePlayer(GameObject player)
	{
		player.SetActive(true);
	}

	// function to disable
	public void disablePlayer(GameObject player)
	{
		player.SetActive(false);
	}
}
