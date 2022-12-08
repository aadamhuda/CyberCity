using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BattleUnit : MonoBehaviour
{

    public int currentHP;
    public int maxHP;
    [SerializeField]
    private int damage;
    public string unitName;
    public Boolean burned;
    public Boolean poisoned;
    [SerializeField]
    private Boolean cursed;
    private Boolean frozen;
    public int type = 0; // 0 is normal (default), 1 is fire, 2 is grass, 3 is water, -1 is no type


    private string[] all_attributes = new string[] { "normal", "shoot", "fire", "water", "ice", "grass", "curse" };
    private Dictionary<string, float> unit_attributes = new Dictionary<string, float>();
    private Dictionary<string, int> unit_attacks = new Dictionary<string, int>();

    public Dictionary<string, float> GetATB () { return unit_attributes;  }

    public Dictionary<string, int> GetATK () { return unit_attacks;  }

    public string [] GetAllATB() { return all_attributes; }

    public int get_dmg() { return damage; }

    public bool get_cursed() { return cursed; }

    public bool get_frozen() { return frozen; }

    public bool get_burned() { return burned; }

    public bool get_poisoned() { return poisoned; }


    public void set_cursed() { cursed = !cursed; }

    public void set_frozen() { frozen = !frozen; }

    public void set_burned() { burned = !burned; }

    public void set_poisoned() { poisoned = !poisoned; }


    public void define_attributes<T>(T [] symbiosis, string [] attribute, Dictionary<string, T> arr)
    {

        for (int i = 0; i < symbiosis.Length; i++)
            arr.Add(attribute[i], symbiosis[i]);
    }


    public void takeDamage(float dmg, string damageType)
    {
        float multiplier = unit_attributes[damageType];

        if (cursed)
            multiplier += 0.15f;

        currentHP -= Mathf.RoundToInt(dmg * multiplier);

        if (currentHP < 0)
            currentHP = 0;
    }

    public bool CheckIfDead()
    {
        if (currentHP == 0)
            return true;
        return false;
    }

    public void usePotion(int amnt)
    {
        currentHP += amnt;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }


    public void disableUnit()
    {
        gameObject.SetActive(false);
    }

    public void RemoveAilments()
    {
        if (burned)
            if (get_rand_number() < 26)
                set_burned();

        if (poisoned)
            if (get_rand_number() < 26)
                set_poisoned();
    }

    private int get_rand_number ()
    {
        return UnityEngine.Random.Range(0, 100);
    }
}
