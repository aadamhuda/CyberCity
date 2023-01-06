using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionCharacter : MonoBehaviour
{
    public GameObject Charcter;
    public CharacterController controller;
    public SaveData savedata;

    public void Change()
    {
        // Disable script to move object
        this.controller.enabled = false;
        this.Charcter.transform.position = this.savedata.get_respawn_location();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Re-anable script when player can input
            this.controller.enabled = true;
            Time.timeScale = 1f;
        }
    }

}