using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject battleSystem;

    public GameObject abilitiesHUD;
    public GameObject abilityButtonPrefab;

    public RectTransform cancelButtonPos;
    public GameObject cancelButtonPrefab;
    public GameObject cancelButton;

    private GameObject[] abilities;

    public void InitaliseMenu(Player currPlayer)
    {
        Transform abilitiesHUDPos = abilitiesHUD.GetComponent<Transform>();
        string[] attacks = currPlayer.allAttacks();
        abilities = new GameObject[attacks.Length];
        for (int i = 0; i < attacks.Length;  i++)
        {
            GameObject abilityButton = Instantiate(abilityButtonPrefab, new Vector3(abilitiesHUDPos.position.x, abilitiesHUDPos.position.y + (i*50), abilitiesHUDPos.position.z), abilitiesHUDPos.rotation, abilitiesHUDPos);
            abilities[i] = abilityButton;
            abilityButton.GetComponent<AbilityButtonController>().updateAbilityName(attacks[i], currPlayer);
        }

        cancelButton = Instantiate(cancelButtonPrefab, cancelButtonPos);

    }

    public void DestroyMenu()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            Destroy(abilities[i]);
        }
        Destroy(cancelButton);

    }

    public void DisplayAbilities()
    {
        abilitiesHUD.SetActive(true);
    }

    public void HideAbilities()
    {
        abilitiesHUD.SetActive(false);
    }



}
