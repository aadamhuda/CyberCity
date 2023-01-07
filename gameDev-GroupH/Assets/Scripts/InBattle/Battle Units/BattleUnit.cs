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
    [SerializeField]
    protected int ID;


    private static string[] all_attributes = new string[] { "normal", "shoot", "fire", "water", "ice", "grass", "curse" };
    private Dictionary<string, float> unit_attributes = new Dictionary<string, float>();
    private Dictionary<string, int> unit_attacks = new Dictionary<string, int>();

    public int getID() { return ID; }
    // All attributes
    public Dictionary<string, float> GetATB () { return unit_attributes;  }

    // Attacks of unit
    public Dictionary<string, int> GetATK () { return unit_attacks;  }

    // Attributes of unit
    public static string [] GetAllATB() { return all_attributes; }

    // Dmg of units
    public int get_dmg() { return damage; }

    // Check of units cursed
    public bool get_cursed() { return cursed; }

    // Check is unit id frozen
    public bool get_frozen() { return frozen; }

    // Check if unity is burned
    public bool get_burned() { return burned; }

    // Check if unity is poisoned
    public bool get_poisoned() { return poisoned; }

    // Curses or removes curse
    public void set_cursed( bool set_value ) { cursed = set_value; }

    // frozen or removes frozen
    public void set_frozen(bool set_value) { frozen = set_value; }

    // burned or removes burned
    public void set_burned(bool set_value) { burned = set_value; }

    // poisoned or removes poisoned
    public void set_poisoned(bool set_value) {poisoned = set_value; }

    // Defines attributes and attacks
    public void define_attributes<T>(T [] symbiosis, string [] attribute, Dictionary<string, T> arr)
    {

        for (int i = 0; i < symbiosis.Length; i++)
            arr.Add(attribute[i], symbiosis[i]);
    }

    // Dmg formula
    // If weak take 25% more dmg
    // If cursed take 15% more dmg
    // If weak to curse and cursed take 30%
    public void takeDamage(float dmg, string damageType)
    {
        float multiplier = unit_attributes[damageType];

        if (cursed)
            if (unit_attributes[damageType] > 1f)
                multiplier += 0.15f;
            else
                multiplier -= 0.15f;
            multiplier += 0.15f;

        currentHP -= Mathf.RoundToInt(dmg * multiplier);

        if (currentHP < 0)
            currentHP = 0;
    }

    // Check if unit is dead --- boolean output
    public bool CheckIfDead()
    {
        if (currentHP == 0)
            return true;
        return false;
    }

    // Makes sure potion recovers health and doesnt exceed health cap
    public void usePotion(int amnt)
    {
        currentHP += amnt;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // Disables unit
    public void disableUnit()
    {
        gameObject.SetActive(false);
    }

    // Removes ailments, 33% of removing aillemnt per round
    public void RemoveAilments()
    {
        if (burned)
            if (get_rand_number() < 26)
                set_burned(false);

        if (poisoned)
            if (get_rand_number() < 26)
                set_poisoned(false);
    }

    // Random number generator -- saves coding time
    private int get_rand_number ()
    {
        return UnityEngine.Random.Range(0, 100);
    }
}
