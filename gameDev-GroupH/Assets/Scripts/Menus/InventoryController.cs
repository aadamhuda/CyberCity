using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //generate a list of items
    [SerializeField]
    private Inventory itemInventory;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private Sprite[] iconSprites;

    [SerializeField]
    GameObject text_field;

    [SerializeField]
    GameObject pause;

    [SerializeField]
    private Transform parent;


    [SerializeField]
    private GameObject button;

    public void LoadMenu()
    {



        foreach (KeyValuePair<string, int> i in itemInventory.get_items())
        {
            // Creates 'plate' for every item in inventory
            GameObject temp = Instantiate(button, parent);

            // Appropriately named 
            temp.name = i.Key;
            // Make the image cleaner
            GameObject temp_text_obj = Instantiate(text_field , temp.transform);

            // Adds text
            TextMeshProUGUI temp_text = temp_text_obj.GetComponent<TextMeshProUGUI>();

            // Adds name of item and quantity
            temp_text.text = i.Key + " x" + i.Value;

            // Positioning variables
        }
    }


    //generates the buttons in the inventory


}
