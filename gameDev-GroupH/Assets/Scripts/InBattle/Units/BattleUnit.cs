using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BattleUnit : MonoBehaviour
{

    public float currentHP;
    public float maxHP;
    public float damage;
    public string unitName;
    public Boolean burned;
    public float burnDamage;
    public float burnMultiplier; //percentage multiplier
    public Boolean frozen;

    // Start is called before the first frame update
    void Start()
    {
        burnMultiplier = 1; // change to weakness
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool takeDamage(float dmg)
    {

        currentHP -=  (float)Math.Round(dmg);

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

}
