using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
	// Values
	public int numberPickups = 5;
	public GameObject surface;

	public Dictionary<string, float> items = new Dictionary<string, float>(); // Item, Probability (should add to one)

	// Start is called before the first frame update
	void Start()
	{
		items.Add("revive", 0.25f); // 25%
		items.Add("maxRevive", 0.35f); // 10%
		items.Add("potion", 0.6f); // 25% 
		items.Add("maxPotion", 0.7f); // 10%
		items.Add("ether", 0.95f); // 25% 
		items.Add("maxEther", 1f); // 5%

		// Get parent 
		GameObject baseItem = this.gameObject.transform.GetChild(0).gameObject;
		GameObject parent = this.gameObject;

		// Get parent bounds
		//Vector3 bounds = GetComponent<Renderer>().bounds.size
		//Vector3 bounds = surface.transform.TransformPoint(new Vector3(1, 1, 1));
		Vector3 baseCoods = surface.transform.position;
		float xScale = surface.transform.localScale.x / 2;
		float zScale = surface.transform.localScale.z / 2;

		// Y pos
		float yy = surface.transform.position.y + surface.transform.localScale.y / 2; // Floor
		yy += baseItem.transform.localScale.z / 2; // Add Item offset

		// Spawn items randomly 
		for (int i = 0; i < numberPickups; i++)
		{
			GameObject newItem = (GameObject)GameObject.Instantiate(baseItem);
			newItem.transform.SetParent(parent.transform, false);

			// Choose pos on surface
			float xx = Random.Range(-xScale, xScale);
			float zz = Random.Range(-zScale, zScale);

			// Choose position
			Vector3 pos = new Vector3(baseCoods.x + xx, yy, baseCoods.z + zz);
			newItem.transform.position = pos;

			// Generate item
			float itemIndex = Random.Range(0f, 1f);
			//Debug.Log(itemIndex);

			// Find item
			foreach (KeyValuePair<string, float> item in items)
			{
				if (item.Value > itemIndex)
				{
					// Set item
					//Debug.Log("Generated: " + item.Key + " from "+ itemIndex);
					ItemPickUp scr = newItem.transform.GetComponent<ItemPickUp>();
					scr.itemName = item.Key;
					scr.spawner = newItem;

					// Break when found
					break;
				}
			}
		}

		// Delete base item
		Destroy(baseItem);
	}
}
