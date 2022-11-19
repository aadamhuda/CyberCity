using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//inherits from parent class BattleUnit
public class Player : BattleUnit
{
	// Player attacks
    public Dictionary<string, int[]> playerAttacks = new Dictionary<string, int[]>();
    public string selectedMove = "burn";

    // Set initial player attacks
    void start()
    {
        currentHP = maxHP;
        
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
        Debug.Log(selectedMove);
    }

    public void heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}
}
