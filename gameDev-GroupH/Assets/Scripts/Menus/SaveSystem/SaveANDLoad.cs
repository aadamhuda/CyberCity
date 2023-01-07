using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveANDLoad : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message;
    public void ChangeText(string msg) { this.message.text = msg; }
    private GameObject obj;

    // Object used to display message about save/load
    public void Activate(string msg, GameObject obj) 
    {
        this.obj = obj;
        this.gameObject.SetActive(true);
        this.obj.SetActive(false);
        this.ChangeText(msg);
    }

    // Disable object on player click and re-enable object which was disabled
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.Disable();
            this.obj.SetActive(true);
        }
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
