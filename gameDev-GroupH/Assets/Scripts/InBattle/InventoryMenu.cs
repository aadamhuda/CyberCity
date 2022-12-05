using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
	public GameObject inventoryMenu;
	private List<Button> items = new List<Button>();
	
	private string currentItem;
	
	public Player[] players;
	public string[] playerNames;
	private List<Button> playerButtons = new List<Button>();
	
	public GameObject itemPrefab;
	public GameObject playerButtonPrefab;

	public RectTransform itemTitlePos;
	public RectTransform playerTitlePos;

	public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Item, Quantity	
	
    // Start is called before the first frame update
    public void LoadMenu(Player[] players, string[] playerNames)
    {
		this.players = players;
		this.playerNames = playerNames;
		
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
	
	void fillPlayers() {
		// Create player buttons
		int i = 0;
		foreach (Player player in players) {
			GameObject newPlayer = GameObject.Instantiate(playerButtonPrefab, new Vector3(playerTitlePos.position.x, playerTitlePos.position.y + (i * -70), playerTitlePos.position.z), playerTitlePos.rotation, playerTitlePos);

			TextMeshProUGUI playerName = newPlayer.GetComponentInChildren<TextMeshProUGUI>(true);

			// Set text
			playerName.text = playerNames[i];
			
			playerButtons.Add(newPlayer.GetComponent<Button>());
			i++;
		}

		disableButtons();
		
	}
	
	void fillInv() {
		// Create item buttons
		int i = 0;
		foreach (KeyValuePair<string, int> entry in inventory) {
			i++;
			GameObject newItem = GameObject.Instantiate(itemPrefab, new Vector3(itemTitlePos.position.x, itemTitlePos.position.y + (i * -70), itemTitlePos.position.z), itemTitlePos.rotation, itemTitlePos);
			
			TextMeshProUGUI[] textFields = newItem.GetComponentsInChildren<TextMeshProUGUI>(true);
			
			// Set text
			textFields[0].text = entry.Key;
			
			// Set count
			textFields[1].text = "x" + entry.Value.ToString();
			
			// Store button
			items.Add(newItem.GetComponent<Button>());
			
		}
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

		this.inventoryMenu.SetActive(false);

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

	 void enableButtons()
    {
		for (int i = 0; i < playerButtons.Count; i++)
		{
			playerButtons[i].interactable = false;
		}
    }

	void disableButtons()
	{
		for (int i = 0; i < playerButtons.Count; i++)
		{
			playerButtons[i].interactable = false;
		}

	}

    public void OnBackButton()
    {
		this.inventoryMenu.SetActive(false);
	}
}
