using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemNotification : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI ItemName;

    // Update is called once per frame
    private void disable()
    {
        this.gameObject.SetActive(false);
    }

    public void Activate(string itemanme)
    {
        this.ItemName.text = itemanme;
        
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }
    }
}
