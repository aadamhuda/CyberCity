using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public string itemName = "null";
    public GameObject spawner;
    bool in_range = false;

    [SerializeField]
    Inventory inven;

    //check user is within range
    private void OnTriggerEnter(Collider Other)
    {
        in_range = true;
    }

    private void OnTriggerExit(Collider Other)
    {
        //Debug.Log("a");
        in_range = false;
    }

    private void Update()
    {
        if (in_range == true)
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                // Add item to Inventory
                inven.add_item(this.itemName);
                gameObject.SetActive(false);
                spawner.GetComponent<ItemSpawn>().decrease_items();
            }
        }


    }
}