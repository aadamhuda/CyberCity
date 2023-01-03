using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementInformation : MonoBehaviour
{
    [SerializeField]
    private GameObject BaseComponent;

    [SerializeField]
    private GameObject PlayerParent;

    [SerializeField]
    private GameObject EnemyParent;
    public void LoadElements(Player player, Dictionary<string, string> enemy, string UnitName)
    {
        this.gameObject.SetActive(true);

        this.PlayerParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.unitName;
        this.EnemyParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = UnitName;

        for (int i = 1; i <  this.PlayerParent.transform.childCount; i++)
        {
            this.PlayerParent.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = ( BattleUnit.GetAllATB()[i-1] + " -- " + PlayerFindValue(player.GetATB()[BattleUnit.GetAllATB()[i-1]]) );

            this.EnemyParent.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (BattleUnit.GetAllATB()[i-1] + " -- " + EnemyFindValue(enemy , BattleUnit.GetAllATB()[i-1]));
        }
    }

    private string PlayerFindValue(float value)
    {
        if (value < 1f)
            return "STRONG";
        if (value > 1f)
            return "WEAK";
        return "NORMAL";
    }

    private string EnemyFindValue(Dictionary<string, string> arr , string key)
    {
        if (arr.ContainsKey(key))
            return (arr[key]).ToUpper();
        return "?";
    }

    public void ReturnToBattle()
    {
        this.gameObject.SetActive(false);
    }
}
