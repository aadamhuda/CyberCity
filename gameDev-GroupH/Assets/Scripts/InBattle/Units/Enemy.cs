using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class BattleUnit
public class Enemy : BattleUnit
{
    // function to enable - used on death of enemy sprite

    private void Start()
    {
        base.Start();
        currentHP = maxHP;
    }
    public void enableEnemy()
	{
		gameObject.SetActive(true);
	}



}