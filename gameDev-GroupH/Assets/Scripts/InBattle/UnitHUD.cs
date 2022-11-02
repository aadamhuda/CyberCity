using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitHUD : MonoBehaviour
{
    public TextMeshProUGUI unitDisplayName;
    public TextMeshProUGUI unitHP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHUD(BattleUnit unit)
    {
        unitDisplayName.text = unit.unitName;
        unitHP.text = unit.currentHP + "/" + unit.maxHP;
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
