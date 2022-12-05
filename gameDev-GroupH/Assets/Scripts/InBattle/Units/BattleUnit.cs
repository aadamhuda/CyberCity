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
    public Boolean poisoned;
    public float poisonDamage;
    public Boolean cursed;
    public float burnMultiplier; // percentage multiplier
    public Boolean frozen;
    public int type = 0; // 0 is normal (default), 1 is fire, 2 is grass, 3 is water, -1 is no type
    public Dictionary<int, int[]> strengths = new Dictionary<int, int[]>();
    public Dictionary<int, int[]> weaknesses = new Dictionary<int, int[]>();

    // Start is called before the first frame update
    public void Start()
    {
        burnMultiplier = 1; // change to weakness
        strengths.Add(0, new int[] { -1 }); // list that it is strong against, list that it is weak against
        strengths.Add(1, new int[] { 2 }); // fire
        strengths.Add(2, new int[] { 3 }); // grass
        strengths.Add(3, new int[] { 1 }); // water
        weaknesses.Add(0, new int[] { -1 }); // list that it is strong against, list that it is weak against
        weaknesses.Add(1, new int[] { 3 }); // fire
        weaknesses.Add(2, new int[] { 1 }); // grass
        weaknesses.Add(3, new int[] { 2 }); // water
    }

    // override


    public bool takeDamage(float dmg, int damageType)
    {
        float typeMultiplier = getMultiplier(damageType);

        if (cursed)
        {
            currentHP -= (int)((float)Math.Round(dmg) * typeMultiplier * 1.1f);

        }
        else
        {
            currentHP -= (int)((float)Math.Round(dmg) * typeMultiplier);

        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
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

    float getMultiplier(int dmgType)
    {

        int[] weakTo = strengths[type];
        int[] strongTo = weaknesses[type];

        // If weak to
        if (weakTo[0] != -1)
        {
            foreach (int x in weakTo)
            {
                if (weakTo[x] == dmgType)
                {
                    return 1.5f;
                }
            }
        }
        if (strongTo[0] != -1)
        {
            // If strong against
            foreach (int x in strongTo)
            {
                if (strongTo[x] == dmgType)
                {
                    return 0.5f;
                }
            }
        }
        


        // If no type advantage, return 1
        return 1;

    }

}
