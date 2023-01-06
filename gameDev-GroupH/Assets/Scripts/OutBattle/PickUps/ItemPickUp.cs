using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ItemPickUp : MonoBehaviour
{
    public string itemName = "null";
    public GameObject spawner;
    bool in_range = false;

    [SerializeField]
    Inventory inven;

    [SerializeField]
    private GameObject indicator;

    [SerializeField]
    private ItemNotification PickUp;


    //check user is within range
    private void OnTriggerEnter(Collider Other)
    {
        in_range = true;
    }

    private void OnTriggerExit(Collider Other)
    {
        in_range = false;
    }

    private void Update()
    {
        // If user is in range display inidcator
        if (in_range == true)
        {
            this.indicator.SetActive(true);
           
        }
        else
            this.indicator.SetActive(false);

    }

    void OnCollectItem(InputValue value)
    {
        if (in_range == true)
        {
            // Only activate in range
            // Add items to player invetory and removes itself
            // Decreases amount of items that spawn on the map
            if (value.isPressed)
            {
                this.PickUp.Activate(this.itemName);
                inven.add_item(this.itemName);
                this.gameObject.SetActive(false);
                spawner.GetComponent<ItemSpawn>().decrease_items();

            }
        }


    }
}