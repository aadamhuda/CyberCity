using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


//inherits from parent class BattleUnit
public class Player : BattleUnit
{
    [SerializeField]
    private int ID;
	// Player attacks
    public Dictionary<string, int[]> playerAttacks = new Dictionary<string, int[]>();
    public string selectedMove = "burn";
    public bool downed;

    // Set initial player attacks


    public int getID()
    {
        return ID;
    }

    public void setHealth(float fml)
    {
        currentHP = fml;
    }

    // Get All Attacks
    string[] allAttacks()
    {
        return (new List<string>(playerAttacks.Keys).ToArray());
    }

    // Change attack
    public void changeAttack()
    {
        string[] attacks = allAttacks();
        int attacksNum = attacks.Length;


        for (int i = 0; i < attacksNum; i++)
        {
            if (attacks[i] == selectedMove)
            {
                Debug.Log(attacks[i]);
                if (i + 1 < attacksNum)
                {
                    selectedMove = attacks[i + 1];
                }
                else
                {
                    selectedMove = attacks[0];
                }
                break;
            }
        }
        TextMeshProUGUI indicator = GameObject.FindWithTag("attackIndicator").GetComponent<TextMeshProUGUI>();
        indicator.text = selectedMove;
        Debug.Log(selectedMove);
    }

    public void revive(float percentage) // can heal 50 or 100 percent for max revive
    {
        this.downed = false;
        this.usePotion((int)Math.Round((float)this.maxHP * percentage));
    }

    public void heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

    public float getTotalMaxHP()
    {
        return maxHP;
    }
}
