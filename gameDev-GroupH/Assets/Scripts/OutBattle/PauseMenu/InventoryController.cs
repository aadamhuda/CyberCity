using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //generate a list of items
    private List<PlayerItem> itemInventory;
    public int amountItems = 8;

    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    [SerializeField]
    private Sprite[] iconSprites; 

    private void Start()
    {
        itemInventory = new List<PlayerItem>();

        //generates a random icon to the button
        for (int i = 1; i < amountItems; i++)
        {
            PlayerItem newItem = new PlayerItem();
            newItem.iconSprite = iconSprites[Random.Range(0, iconSprites.Length)];

            itemInventory.Add(newItem);
        }

        GenerateInventory();
    }

    //generates the buttons in the inventory
    void GenerateInventory()
    {
        if(itemInventory.Count < amountItems)
        {
            gridGroup.constraintCount = itemInventory.Count;
        } else
        {
            gridGroup.constraintCount = amountItems - 1;
        }

        foreach(PlayerItem newPlayerItem in itemInventory)
        {
            GameObject newItemButton = Instantiate(buttonTemplate) as GameObject;
            newItemButton.SetActive(true);

            newItemButton.GetComponent<InventoryButton>().SetIcon(newPlayerItem.iconSprite);
            newItemButton.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    public class PlayerItem
    {
        public Sprite iconSprite;
    }
}
