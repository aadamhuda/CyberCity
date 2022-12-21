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

        int start_x = -500;
        int start_y = 100;


        foreach (KeyValuePair<string, int> i in itemInventory.get_items())
        {

            GameObject temp = Instantiate(new GameObject() , new Vector3( start_x + gameObject.transform.position.x , start_y + gameObject.transform.position.y , 0 ) , Quaternion.identity , gameObject.transform);
            temp.name = i.Key;
            Image temp_image = temp.AddComponent<Image>();
            temp_image.sprite = sprite;
            temp_image.type = Image.Type.Sliced;
            GameObject temp_text_obj = Instantiate(text_field , temp.transform);
            TextMeshProUGUI temp_text = temp_text_obj.GetComponent<TextMeshProUGUI>();
            temp_text.text = i.Key + " x" + i.Value;
            start_y -= 250;
            if (start_y < -150)
            {
                start_x += 250;
                start_y = 100;
            }
        }
    }


    //generates the buttons in the inventory


}
