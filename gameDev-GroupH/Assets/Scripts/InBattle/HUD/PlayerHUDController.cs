using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpValue;

    public Slider mpSlider;
    public TextMeshProUGUI mpValue;



    public void SetHealth(Player player)
    {
        hpSlider.value = player.currentHP;
        hpValue.text = player.currentHP.ToString();
    }

    public void InitialiseSlider(Player player)
    {
        hpSlider.maxValue = player.maxHP;
        Debug.Log(player.maxMP);
        mpSlider.maxValue = player.maxMP;
    }

    public void SetMagic(Player player)
    {
        mpSlider.value = player.currentMP;
        mpValue.text = player.currentMP.ToString();
    }

}
