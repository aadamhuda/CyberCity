using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class BattleUnit
public class Enemy : BattleUnit
{


	// function to enable - used on death of enemy sprite
	public void enableEnemy()
	{
		gameObject.SetActive(true);
	}

	// function to disable
	public void disableEnemy()
	{
		gameObject.SetActive(false);
	}
}
