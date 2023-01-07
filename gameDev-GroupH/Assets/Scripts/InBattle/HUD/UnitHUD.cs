using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitHUD : MonoBehaviour
{
    public TextMeshProUGUI unitDisplayName;
    public TextMeshProUGUI unitHP;
    public Slider hpSlider;


    public void updateHUD(BattleUnit unit)
    {
        unitDisplayName.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        SetHealth(unit);
    }

    public void SetHealth(BattleUnit unit)
    {
        hpSlider.value = unit.currentHP;
        unitHP.text = unit.currentHP.ToString();
    }
    public void InitialiseSlider(BattleUnit unit)
    {
        hpSlider.maxValue = unit.maxHP;
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
