using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //List of items
    [SerializeField]
    private Dictionary<string,int> items = new Dictionary<string, int>();

    [SerializeField]
    private SaveData savedata;

    [SerializeField]
    protected Sprite icon_sprite;


    private void Start()
    {
        // Checks if there already items saved 
        if (savedata.get_items().Count > 0)
            items = savedata.get_items();
        //adds items for the player to test them in battle - only use in testing
        //AddTestItems();
    }

    public Sprite GetSprite() { return this.icon_sprite; }
    public Dictionary<string, int> get_items() { return items; }

    //Add items
    public void add_item(string new_item)                                                                                           
    {
        // Adds item to inventory
        if (items.ContainsKey(new_item))
            items[new_item] += 1;
        else
            items.Add(new_item, 1);
        // Saves data into sciptable object
        savedata.set_items(this.items);      
        
    }

    public void AddTestItems()
    {
        // Test inventory
        add_item("smallPotion");
        add_item("smallPotion");
        add_item("smallPotion");

        add_item("maxPotion");

        add_item("revive");
        add_item("revive");
        add_item("maxRevive");

        add_item("ether");
        add_item("ether");
        add_item("maxEther");
    }
}
