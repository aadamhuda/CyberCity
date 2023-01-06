using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemNotification : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI ItemName;

    [SerializeField]
    private Inventory inventory;

    private Queue<string> notifications = new Queue<string>();
    
    public void Activate(string itemanme)
    {
        // Activate when player picks up item
        this.gameObject.SetActive(true);
        // If item exist add '+1' otherwise add item to queue
        if (inventory.get_items().ContainsKey(itemanme))
            this.notifications.Enqueue(itemanme + "+1");
        else
            this.notifications.Enqueue(itemanme);
    }

    private void Update()
    {
        // Iterate through queue until finished so the user can see all the items
        // if multiple items we're picked up
        if (this.notifications.Count > 0)
        {
            this.ItemName.text = this.notifications.Peek();
            if (Input.GetMouseButtonDown(0))
            {
                this.notifications.Dequeue();
                if (notifications.Count == 0)
                    this.gameObject.SetActive(false);
            }
        }

    }
}
