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

	public Sprite[] itemImages;
	[SerializeField]
	private GameObject itemImage;

	[SerializeField]
	private InventorySystem inventory; // Item, Quantity	

    public void LoadMenu(Player[] players, string[] playerNames)
    {

		this.players = players;
		this.playerNames = playerNames;

		items.Clear();
		playerButtons.Clear();
		itemImage.SetActive(false);
		cancelButton.interactable = false;
		inventory.define_inventory();
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
		foreach (KeyValuePair<string, int> entry in inventory.get_inventory()) {
			Debug.Log("Edgeo ajdsb the : " + entry.Key);
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
			case "ether":
				DownedButtonFilter(playerButtons, false);
				break;
			case "maxEther":
				DownedButtonFilter(playerButtons, false);
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
			case "ether":
				player.GetComponent<Player>().Restore(20);
				break;
			case "maxEther":
				player.GetComponent<Player>().Restore(999999999);
				break;
			default:
				break;
		}

		DestroyMenuButtons(items);
		DestroyMenuButtons(playerButtons);
		battleSystem.GetComponent<BattleSystem>().UseItem();

	}

	void ItemImageUpdate()
    {
		itemImage.SetActive(true);
		switch (currentItem)
		{
			case "smallPotion":
				itemImage.GetComponent<Image>().sprite = itemImages[0];
				break;
			case "maxPotion":
				itemImage.GetComponent<Image>().sprite = itemImages[1];
				break;
			case "revive":
				itemImage.GetComponent<Image>().sprite = itemImages[2];
				break;
			case "maxRevive":
				itemImage.GetComponent<Image>().sprite = itemImages[3];
				break;
			case "ether":
				itemImage.GetComponent<Image>().sprite = itemImages[4];
				break;
			case "maxEther":
				itemImage.GetComponent<Image>().sprite = itemImages[5];
				break;
			default:
				break;
		}
	}
	// Use item
	public void useItem(string item) {
			
		// Decrement quantity
		inventory.get_inventory()[item] -= 1; // Decrement


		// Remove if 0
		if (inventory.get_inventory()[item] == 0) {
			inventory.remove_item(item);
		}
	}
	
	// Add item
	public void addItem(string item) {
		inventory.addItem(item , 1);
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
		ItemImageUpdate();
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
		for (int i = 0; i < buttons.Count; i++)
		{
			Destroy(buttons[i].gameObject);
		}

		buttons.Clear();
	}

    public void OnBackButton()
    {
		DestroyMenuButtons(items);
		DestroyMenuButtons(playerButtons);
		this.inventoryMenu.SetActive(false);
	}
	public void OnCancelButton()
	{
		currentItem = "";

		disableButtons(playerButtons);
		enableButtons(items);
		itemImage.SetActive(false);
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

		addItem("ether");
		addItem("ether");
		addItem("maxEther");
	}


}
