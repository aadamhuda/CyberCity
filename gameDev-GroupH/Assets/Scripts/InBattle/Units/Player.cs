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
    public Dictionary<string, int> playerAttacks = new Dictionary<string, int>();
    public string selectedMove = "normal";
    public bool downed;

    // Set initial player attacks

    private void Start()
    {
        float norm = 1f;
        float weakness = 1.25f;
        float strength = 0.75f;

        if (ID == 0)
        {
            base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), 0, base.get_dmg() - 10 }, new string[] { "normal", "curse", "shoot" }, base.GetATK());
        }
        else if (ID == 1)
        {
            //base.playerAttacks.Add("burn", new int[] { 1, 12, 5 }); 
            base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 10 }, new string[] { "normal", "fire" }, base.GetATK());
        }
        else if (ID == 2)
        {
            //base.playerAttacks.Add("poison", new int[] { -1, 40 });
            base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() - 5 }, new string[] { "normal", "grass" }, base.GetATK());
        }
        else
        {
            base.define_attributes<float>(new float[] { norm, norm, norm, norm, norm, norm, norm }, base.GetAllATB(), base.GetATB());
            base.define_attributes<int>(new int[] { base.get_dmg(), base.get_dmg() + 5, 0 }, new string[] { "normal", "water", "ice" }, base.GetATK());
        }
    }

    public int getID()
    {
        return ID;
    }

    public void setHealth(int fml)
    {
        currentHP = fml;
    }

    // Get All Attacks
    public string[] allAttacks()
    {
        return (new List<string>(base.GetATK().Keys).ToArray());
    }

    // Change attack
    public void changeAttack(string attackName)
    {
        string[] attacks = allAttacks();
        int attacksNum = attacks.Length;


        for (int i = 0; i < attacksNum; i++)
        {
            if (attacks[i] == attackName)
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
        indicator.text = attackName;
        Debug.Log(attackName);
    }

    public void revive(float percentage) // can heal 50 or 100 percent for max revive
    {
        this.downed = false;
        this.usePotion((int)Math.Round((float)this.maxHP * percentage));

        this.gameObject.GetComponent<Animator>().CrossFade("Idle", 0.2f); ;
        
    }

    public void heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

    public int getTotalMaxHP()
    {
        return maxHP;
    }
}
