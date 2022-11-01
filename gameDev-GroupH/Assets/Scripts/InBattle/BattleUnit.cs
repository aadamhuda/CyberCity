using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{

    public float currentHP;
    public float maxHP;
    public float damage;
    public string unitName; 

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool takeDamage(float dmg)
    {

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

}
