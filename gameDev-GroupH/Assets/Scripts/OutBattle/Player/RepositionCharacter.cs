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
        //re enables after moving to save point
        this.controller.enabled = true;
        Time.timeScale = 1f;
    }
    public void Reposition()
    {
        // Disable script to move object
        this.controller.enabled = false;
        this.Charcter.transform.position = this.savedata.get_player_location();
        //re enables after moving to respawn point
        this.controller.enabled = true;
        Time.timeScale = 1f;
    }
}