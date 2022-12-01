using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitHUD : MonoBehaviour
{
    public TextMeshProUGUI unitDisplayName;
    public TextMeshProUGUI unitHP;


    public void updateHUD(BattleUnit unit)
    {
        //unitDisplayName.text = unit.unitName;
        //unitHP.text = unit.currentHP + "/" + unit.maxHP;
    }

    public void enableHUD()
    {
        gameObject.SetActive(true);
    }

    // function to disable
    public void disableHUD()
    {
        gameObject.SetActive(false);
    }
}
