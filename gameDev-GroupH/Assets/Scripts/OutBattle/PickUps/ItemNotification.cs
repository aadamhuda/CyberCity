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
        this.gameObject.SetActive(true);
        if (inventory.get_items().ContainsKey(itemanme))
            this.notifications.Enqueue(itemanme + "+1");
        else
            this.notifications.Enqueue(itemanme);
    }

    private void Update()
    {
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
