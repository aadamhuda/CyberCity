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


    public void LoadMenu()
    {

        int start_x = 215;
        int start_y = 410;


        foreach (KeyValuePair<string, int> i in itemInventory.get_items())
        {
            // Creates 'plate' for every item in inventory
            GameObject temp = Instantiate(new GameObject() , new Vector3( start_x + gameObject.transform.position.x , start_y + gameObject.transform.position.y , 0 ) , Quaternion.identity , gameObject.transform);

            // Appropriately named 
            temp.name = i.Key;
            Image temp_image = temp.AddComponent<Image>();
            temp_image.sprite = sprite;

            // Make the image cleaner
            temp_image.type = Image.Type.Sliced;
            GameObject temp_text_obj = Instantiate(text_field , temp.transform);

            // Adds text
            TextMeshProUGUI temp_text = temp_text_obj.GetComponent<TextMeshProUGUI>();

            // Adds name of item and quantity
            temp_text.text = i.Key + " x" + i.Value;

            // Positioning variables
            start_y -= 200;
            if (start_y < 10)
            {
                start_x += 150;
                start_y = 360;
            }
        }
    }


    //generates the buttons in the inventory


}
