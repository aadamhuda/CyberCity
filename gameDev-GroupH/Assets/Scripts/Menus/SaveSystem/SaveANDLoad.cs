using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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
    void OnSkip(InputValue value)
    {
        if (value.isPressed)
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
