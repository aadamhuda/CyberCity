using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvPlayer : MonoBehaviour
{
	public InventoryMenu menu;
	public Button thisButton;
	public TextMeshProUGUI playerName;

	// Init
	void Start()
	{
		thisButton.onClick.AddListener(TaskOnClick);
	}

	// Use item
	void TaskOnClick()
	{
		menu.PlayerSelect(playerName.text);
	}
}
