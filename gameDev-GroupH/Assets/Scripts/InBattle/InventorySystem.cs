using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private SaveData savedata;

    private Dictionary<string, int> inventory = new Dictionary<string, int>(); // Item, Quantity
    // NOTE: May change value to array of [category, value, quantity]
    // e.g. "smallPotion", ["healing", 20, 3] 
    // e.g. "maxRevive", ["reviving", 100%, 1]

    // Start is called before the first frame update
    public void define_inventory()
    {
        Debug.Log("Don't even know how it has entries : " + inventory.Count );

        inventory.Clear();




        foreach (KeyValuePair<string , int> i in savedata.get_items())
            addItem(i.Key, i.Value);

        Debug.Log("bruh");

        foreach (KeyValuePair<string, int> i in inventory)
            Debug.Log(i.Key + " + " + i.Value);
    }

    public Dictionary<string , int> get_inventory() { return this.inventory; }

    //// Inventory Functions

    // Use item
    public void useItem(string item)
    {
        int quantity;

        inventory.TryGetValue(item, out quantity); // Store current quantity 

        // Decrement quantity
        inventory[item] = quantity - 1; // Decrement

        // Remove if 0
        if (quantity - 1 <= 0)
        {
            inventory.Remove(item);
        }
    }

    // Add item
    public void addItem(string item , int quantity)
    {
        int amount;
        inventory.TryGetValue(item, out amount);

        if (amount > 0)
            inventory[item] += quantity;
        else
            inventory.Add(item, quantity);
        
    }
}