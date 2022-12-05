using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    public InventoryMenu parent;
	public Button thisButton;
	public string itemName;

	// Init
	public void Init() {
		thisButton.onClick.AddListener(TaskOnClick);
	}

    // Use item
    void TaskOnClick(){
		parent.PlayerSelect(itemName);
	}
}
