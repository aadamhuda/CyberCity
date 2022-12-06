using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hpValue;



    public void SetHealth(Player player)
    {
        slider.value = player.currentHP;
        hpValue.text = player.currentHP.ToString();
    }

    public void InitialiseSlider(Player player)
    {
        slider.maxValue = player.maxHP;
    }
}
