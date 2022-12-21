using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

	[SerializeField]
	private LayerMask [] surface;
	[SerializeField]
	private SaveData savedata;

	private Dictionary<string, float> items = new Dictionary<string, float>(); // Item, Probability (should add to one)

	private int remaining_items;

	// Start is called before the first frame update
	void Start()
	{
		// List of all items, and drop chance
		items.Add("revive", 0.25f); // 25%
		items.Add("maxRevive", 0.35f); // 10%
		items.Add("smallPotion", 0.6f); // 25% 
		items.Add("maxPotion", 0.7f); // 10%
		items.Add("ether", 0.95f); // 25% 
		items.Add("maxEther", 1f); // 5%

		// Get parent 
		GameObject baseItem = this.gameObject.transform.GetChild(0).gameObject;
		GameObject parent = this.gameObject;

		int max = 70;
		// Edge of map

		remaining_items = savedata.get_item_respawn();
		Debug.Log(remaining_items);
            

		// Y pos

		// Spawn items randomly 
		for (int i = 0; i < remaining_items; i++)
		{

			Vector3 baseCoods = new Vector3(Random.Range(max, -max), 1, Random.Range(max, -max));
			//Rnd vector within map
			while (Physics.Raycast(baseCoods, -transform.up, 3f, surface[Random.Range(0, surface.Length)]) == false)
				baseCoods = new Vector3(Random.Range(max, -max), 0, Random.Range(max, -max));

			// Instantiate item within map
			bool check = Physics.Raycast(baseCoods, -transform.up, 3f, surface[i]);


			GameObject newItem = Instantiate(baseItem, baseCoods, Quaternion.identity, parent.transform);
			newItem.name = "item";


			// Generate item
			float itemIndex = Random.Range(0f, 1f);
			//Debug.Log(itemIndex);

			// Assigning and item to item
			foreach (KeyValuePair<string, float> item in items)
			{
				if (item.Value > itemIndex)
				{
					// Set item
					//Debug.Log("Generated: " + item.Key + " from "+ itemIndex);
					ItemPickUp scr = newItem.transform.GetComponent<ItemPickUp>();
					scr.itemName = item.Key;
					scr.spawner = this.gameObject;

					// Break when found
					break;
				}
			}

			
		}

		

		// Delete base item
		Destroy(baseItem);
	}

	public void decrease_items() { this.remaining_items--; savedata.set_item_respawn(this.remaining_items); }
}
