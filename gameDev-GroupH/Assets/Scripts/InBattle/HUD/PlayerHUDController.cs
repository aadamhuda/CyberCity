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

    //sets the health of the player on text and slider value
    public void SetHealth(Player player)
    {
        hpSlider.value = player.currentHP;
        hpValue.text = player.currentHP.ToString();
    }

    //sets the max value of the sliders using max MP and HP of the player
    public void InitialiseSlider(Player player)
    {
        hpSlider.maxValue = player.maxHP;
        mpSlider.maxValue = player.maxMP;
    }

    //sets the MP of the player to the text and slider
    public void SetMagic(Player player)
    {
        mpSlider.value = player.currentMP;
        mpValue.text = player.currentMP.ToString();
    }

}
