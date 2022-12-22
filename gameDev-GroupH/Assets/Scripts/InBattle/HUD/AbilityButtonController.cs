using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour
{
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityMP;
    public Button thisButton;
    public string attackType;
    public BattleSystem battleSystem;

    private void Start()
    {
        battleSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<BattleSystem>();
        thisButton.onClick.AddListener(TaskOnClick);
    }
    public void updateAbilityName(string name, Player currPlayer)
    {
        abilityName.text = name;
        attackType = name;
        
        abilityMP.text = currPlayer.mpCost[attackType].ToString();
    }

    // Do attack of Type attackType
    void TaskOnClick()
    {
        battleSystem.OnAttackButton(attackType);
    }
}
