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

    //updates hud, called in update in battlesystem
    public void updateHUD(BattleUnit unit)
    {
        unitDisplayName.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        SetHealth(unit);
    }
    //sets slider value and text value
    public void SetHealth(BattleUnit unit)
    {
        hpSlider.value = unit.currentHP;
        unitHP.text = unit.currentHP.ToString();
    }
    // init slider value with its max value
    public void InitialiseSlider(BattleUnit unit)
    {
        hpSlider.maxValue = unit.maxHP;
    }

    // function to disable
    public void disableHUD()
    {
        gameObject.SetActive(false);
    }
}
