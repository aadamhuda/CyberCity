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
        this.controller.enabled = false;
        this.Charcter.transform.position = this.savedata.get_respawn_location();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.controller.enabled = true;
            Time.timeScale = 1f;
        }
    }

}