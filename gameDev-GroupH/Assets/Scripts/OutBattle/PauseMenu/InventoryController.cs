using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //generate a list of items
    private List<PlayerItem> itemInventory;

    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    [SerializeField]
    private Sprite[] iconSprites; 

    private void Start()
    {
        itemInventory = new List<PlayerItem>();

        //creates 8 items in the list of items in the inventory
        for(int i = 1; i<8; i++)
        {
            PlayerItem newItem = new PlayerItem();
            newItem.IconSprite = iconSprites[Random.Range(0, iconSprites.Length)];

            itemInventory.Add(newItem);
        }
    }

    public class PlayerItem
    {
        public Sprite IconSprite;
    }
}
