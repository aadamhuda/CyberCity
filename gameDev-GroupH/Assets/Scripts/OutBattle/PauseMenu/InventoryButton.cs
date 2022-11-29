using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    //sets image to button
    public void SetIcon(Sprite itemSprite)
    {
        itemIcon.sprite = itemSprite;
    }
}
