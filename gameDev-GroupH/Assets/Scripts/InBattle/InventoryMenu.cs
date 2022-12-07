using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
	public Button cancelButton;
	public GameObject battleSystem;
	public GameObject inventoryMenu;
	private List<Button> items = new List<Button>();
	
	private string currentItem;
	
	public Player[] players;
	public string[] playerNames;
	public List<Button> playerButtons = new List<Button>();
	
	public GameObject itemPrefab;
	public GameObject playerButtonPrefab;

	public RectTransform itemTitlePos;
	public RectTransform playerTitlePos;

	public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Item, Quantity	

    public void LoadMenu(Player[] players, string[] playerNames)
    {
		this.players = players;
		this.playerNames = playerNames;

		items.Clear();
		playerButtons.Clear();

		cancelButton.interactable = false;

		// Draw inv
		fillPlayers();
		fillInv();
    }
	
	void fillPlayers() {
		// Create player buttons
		int i = 0;
		foreach (Player player in players) {

			GameObject newPlayer = GameObject.Instantiate(playerButtonPrefab, new Vector3(playerTitlePos.position.x, playerTitlePos.position.y + ((i+1) * -70), playerTitlePos.position.z), playerTitlePos.rotation, playerTitlePos);

			TextMeshProUGUI playerName = newPlayer.GetComponentInChildren<TextMeshProUGUI>(true);

			// Set text
			playerName.text = playerNames[i];

			newPlayer.GetComponent<InvPlayer>().menu = this;

			playerButtons.Add(newPlayer.GetComponent<Button>());
			i++;
		}

		disableButtons(playerButtons);
		
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
			textFields[1].text = entry.Value.ToString();

			newItem.GetComponent<InvItem>().menu = this;

			// Store button
			items.Add(newItem.GetComponent<Button>());
			
		}
	}
	
	
	// Select item
	public void ItemSelect(string itemName) {
		currentItem = itemName;

		disableButtons(items);
		enableButtons(playerButtons);
		cancelButton.interactable = true;

		switch (currentItem)
		{
			case "smallPotion":
				DownedButtonFilter(playerButtons, false);
				break;
			case "maxPotion":
				DownedButtonFilter(playerButtons, false);
				break;
			case "revive":
				DownedButtonFilter(playerButtons, true);
				break;
			case "maxRevive":
				DownedButtonFilter(playerButtons, true);
				break;
			default:
				break;
		}


	}

	public void PlayerSelect(string playerName)
	{
		for (int i = 0; i< players.Length; i++)
        {
			if(players[i].unitName == playerName)
            {
				UseItemOnPlayer(players[i]);
            }
        }

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

		DestroyMenuButtons(items);
		DestroyMenuButtons(playerButtons);
		battleSystem.GetComponent<BattleSystem>().UseItem();

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

	void DownedButtonFilter(List<Button> buttons, bool active)
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			if(players[i].downed)
            {
				buttons[i].interactable = active;
			}
            else
            {
				buttons[i].interactable = !active;
			}
		}
	}




	void enableButtons(List<Button> buttons)
    {
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].interactable = true;
		}
    }

	void disableButtons(List<Button> buttons)
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].interactable = false;
		}

	}

    void DestroyMenuButtons(List<Button> buttons)
    {
		enableButtons(buttons);
		enableButtons(playerButtons);
		for (int i = 0; i < buttons.Count; i++)
		{
			Destroy(buttons[i].gameObject);
		}

		buttons.Clear();
	}

    public void OnBackButton()
    {
		this.inventoryMenu.SetActive(false);
	}
	public void OnCancelButton()
	{
		currentItem = "";

		disableButtons(playerButtons);
		enableButtons(items);

		cancelButton.interactable = false;
	}

	public void AddTestItems()
    {
		// Test inventory
		addItem("smallPotion");
		addItem("smallPotion");
		addItem("smallPotion");

		addItem("maxPotion");

		addItem("revive");
		addItem("revive");
		addItem("maxRevive");
	}


}
