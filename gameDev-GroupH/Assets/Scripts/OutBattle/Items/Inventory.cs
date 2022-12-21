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

    private Dictionary<string, Sprite> indivisual_item = new Dictionary<string, Sprite>();

    private void Start()
    {

        Debug.Log("Here we go again");
        if (savedata.get_items().Count > 0)
            items = savedata.get_items();
        this.add_item("smallPotion");

    }
    public Dictionary<string, int> get_items() { return items; }

    //Add items
    public void add_item(string new_item)                                                                                           
    {

        if (items.ContainsKey(new_item))
            items[new_item] += 1;
        else
            items.Add(new_item, 1);

        savedata.set_items(this.items);                    
    }
}

public class item : Inventory
{
    [SerializeField]
    private string item_name;

    private Dictionary<string, Sprite> indivisual_item = new Dictionary<string, Sprite>();

    private void Start()
    {
        
    }
    public void set_item(string name)
    {
        this.item_name = name;
        indivisual_item.Add(name , icon_sprite);
    }

    public string get_name() { return this.item_name; }

    public Sprite get_sprite() { return this.indivisual_item[this.item_name]; }

}