using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvItem : MonoBehaviour
{
	public InventoryMenu menu;
	public Button thisButton;
	public TextMeshProUGUI itemName;

	// Init
	void Start()
	{
		thisButton.onClick.AddListener(TaskOnClick);
	}

    // Use item
    void TaskOnClick(){
		menu.ItemSelect(itemName.text);
	}
}
