using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Item, Quantity
    // NOTE: May change value to array of [category, value, quantity]
    // e.g. "smallPotion", ["healing", 20, 3] 
    // e.g. "maxRevive", ["reviving", 100%, 1]

    // Start is called before the first frame update
    void Start()
    {
        // Fill battle inventory from main out-of-battle inventory

        // Test inventory
        addItem("smallPotion");
        addItem("smallPotion");
        addItem("smallPotion");

        addItem("maxPotion");

        addItem("revive");
        addItem("maxRevive");
    }

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
    public void addItem(string item)
    {
        int quantity;

        inventory.TryGetValue(item, out quantity);

        // Already have, then increment
        if (quantity > 0)
        {
            inventory[item] = quantity + 1; // Increment
        }
        // Not in inventory, then add
        else
        {
            inventory.Add(item, 1);
        }
    }
}