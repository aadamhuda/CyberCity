using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BattleUnit
{

	// Start is called before the first frame update
	void Start()
	{
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
}
