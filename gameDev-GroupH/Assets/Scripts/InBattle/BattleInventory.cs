using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleInventory : MonoBehaviour
{
	public Button button;
	
	public Canvas inventoryMenu;
	public Canvas battleButtons;
	private Button[] buttons;
	private List<Button> items = new List<Button>();
	
	private string currentItem;
	
	public Player[] players;
	public string[] playerNames;
	private List<Button> playerButtons = new List<Button>();
	
	public Button baseButton;
	public Button basePlayerButton;
	
	public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Item, Quantity	
	
    // Start is called before the first frame update
    public void Init(Player[] players, string[] playerNames)
    {
		this.players = players;
		this.playerNames = playerNames;
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		
		inventoryMenu.enabled = false;
		buttons = battleButtons.GetComponentsInChildren<Button>();
		
		// Test inventory
		addItem("smallPotion");
		addItem("smallPotion");
		addItem("smallPotion");
		
		addItem("maxPotion");
		
		addItem("revive");
		addItem("revive");
		addItem("maxRevive");
		
		// Draw inv
		fillPlayers();
		fillInv();
    }

	void ToggleBattleButtons(bool type) {
		

		for (int i=0; i<buttons.Length; i++) {
			// Check tag
			
			if (buttons[i].tag == "battleButton") {
				buttons[i].gameObject.SetActive(type);
			}
			
		}
	}


	// Toggle inv
    void TaskOnClick(){
		bool active = inventoryMenu.isActiveAndEnabled;
		
		if (active) {
			inventoryMenu.enabled = false;
			ToggleBattleButtons(true);
		} else {
			inventoryMenu.enabled = true;
			ToggleBattleButtons(false);
		}
	}
	
	void fillPlayers() {
		// Create player buttons
		int i = 0;
		foreach (Player player in players) {
			Button newPlayer = (Button)GameObject.Instantiate(basePlayerButton);
			
			// Dupe 
			var baseRect = basePlayerButton.GetComponent<RectTransform>();
			var rect = newPlayer.GetComponent<RectTransform>();
			rect.anchorMin = baseRect.anchorMin;
			rect.anchorMax = baseRect.anchorMax;
			rect.anchoredPosition = baseRect.anchoredPosition;
			rect.offsetMin = baseRect.offsetMin;
			rect.offsetMax = baseRect.offsetMax;
			
			Vector3 pos = newPlayer.transform.position;
			pos.y -= 30f * i;
			newPlayer.transform.position = pos;
			
			newPlayer.transform.SetParent(inventoryMenu.transform, false);
			
			TextMeshProUGUI playerName = newPlayer.GetComponentInChildren<TextMeshProUGUI>(true);

			// Set text
			playerName.text = playerNames[i];
			
			// Set script parent
			PlayerButtonScript scr = newPlayer.transform.GetComponent<PlayerButtonScript>();
			scr.parent = this;
			scr.thisButton = newPlayer;
			scr.player = player;
			scr.Init();
			
			// Disable
			newPlayer.enabled = false;
			newPlayer.targetGraphic.CrossFadeAlpha(0.2f, 0, false);
			
			playerButtons.Add(newPlayer);
			i++;
		}
		
		// Disable base button
		basePlayerButton.gameObject.SetActive(false);
		
	}
	
	void fillInv() {
		// Create item buttons
		int i = 0;
		foreach (KeyValuePair<string, int> entry in inventory) { 
			Button newItem = (Button)GameObject.Instantiate(baseButton);
			
			// Dupe 
			var baseRect = baseButton.GetComponent<RectTransform>();
			var rect = newItem.GetComponent<RectTransform>();
			rect.anchorMin = baseRect.anchorMin;
			rect.anchorMax = baseRect.anchorMax;
			rect.anchoredPosition = baseRect.anchoredPosition;
			rect.offsetMin = baseRect.offsetMin;
			rect.offsetMax = baseRect.offsetMax;
			
			Vector3 pos = newItem.transform.position;
			pos.y -= 30f * i;
			newItem.transform.position = pos;
			
			newItem.transform.SetParent(inventoryMenu.transform, false);
			
			TextMeshProUGUI[] textFields = newItem.GetComponentsInChildren<TextMeshProUGUI>(true);
			
			// Set text
			textFields[0].text = entry.Key;
			
			// Set count
			textFields[1].text = "x" + entry.Value.ToString();
			
			// Set script parent
			InvItem scr = newItem.transform.GetComponent<InvItem>();
			scr.parent = this;
			scr.itemName = entry.Key;
			scr.thisButton = newItem;
			scr.Init();
			
			// Store button
			items.Add(newItem);
			i++;
		}
		
		// Disable base button
		baseButton.gameObject.SetActive(false);
	}
	
	public void toggleInvButtons(bool val) {
		// Transparencies
		float itemTransparency;
		float playerTransparency;
		
		if (val) {
			itemTransparency = 1f;
			playerTransparency = 0.2f;
		} else {
			itemTransparency = 0.2f;
			playerTransparency = 1f;
		}
		
		// Toggle Inv
		for (int i=0; i < items.Count; i++) {
			items[i].enabled = val;
			items[i].targetGraphic.CrossFadeAlpha(itemTransparency, 0, false);
		}
		
		// Toggle Players
		for (int i=0; i < playerButtons.Count; i++) {
			playerButtons[i].enabled = (!val);
			playerButtons[i].targetGraphic.CrossFadeAlpha(playerTransparency, 0, false);
		}
	}
	
	// Select a player
	public void PlayerSelect(string itemName) {
		currentItem = itemName;
		//Debug.Log("Selected item: "+ itemName);
		
		// Disable inv buttons
		toggleInvButtons(false);
	
	}

	// Use item on player
	public void UseItemOnPlayer(Player player)
	{
		// Remove item from inventory
		useItem(currentItem);

		// Use items
		switch (currentItem)
		{
			case "smallPotion":
				player.GetComponent<BattleUnit>().usePotion(20);
				break;
			case "maxPotion":
				player.GetComponent<BattleUnit>().usePotion(999999999);
				break;
			case "revive":
				player.GetComponent<Player>().revive(0.5f);
				break;
			case "maxRevive":
				player.GetComponent<Player>().revive(1f);
				break;
			default:
				break;
		}

		// Toggle inv buttons
		toggleInvButtons(true);

		// Redraw Quantities
		redraw();
	}

	private void redraw() {
		List<Button> removedItems = new List<Button>();
		
		foreach (Button item in items) {
			string itemName = item.transform.GetComponent<InvItem>().itemName;
			
			int count;
			inventory.TryGetValue(itemName, out count); // Store current quantity 
	
			TextMeshProUGUI[] textFields = item.GetComponentsInChildren<TextMeshProUGUI>(true);
				
			// Set count
			textFields[1].text = "x" + count.ToString();
	
			if (count <= 0) {
				removedItems.Add(item);
			}
		}
		
		foreach (Button item in removedItems) {
			item.enabled = false;
			item.targetGraphic.CrossFadeAlpha(0.1f, 0, false);
		}
	}
	
	
	
	
	// Use item
	public void useItem(string item) {
		int quantity;
		
		inventory.TryGetValue(item, out quantity); // Store current quantity 
			
		// Decrement quantity
		inventory[item] = quantity - 1; // Decrement
		
		// Remove if 0
		if (quantity - 1 <= 0) {
			inventory.Remove(item);
		}
	}
	
	// Add item
	public void addItem(string item) {
		int quantity;
		
		inventory.TryGetValue(item, out quantity);
		
		// Already have, then increment
		if (quantity > 0) {
			inventory[item] = quantity + 1; // Increment
		}
		// Not in inventory, then add
		else {
			inventory.Add(item, 1);
		}
	}
}
