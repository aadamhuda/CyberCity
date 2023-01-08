using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    bool cybercity = false;

    [SerializeField]
    private SaveData savedata;

    [SerializeField]
    private GameObject EndScreen;

    [SerializeField]
    private GameObject ReturnScreen;
    
    // Button to return to Main Menu
    public void ReturnButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Check if player is on pad
    private void OnTriggerEnter(Collider collision)
    {
        this.cybercity = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        this.cybercity = false;
        this.ReturnScreen.SetActive(false);
    }

    void OnTeleport(InputValue value)
    {
        if (this.cybercity)
        {

            if (value.isPressed)
            {
                // Check which screen to activate depending on the amount of clues picked up
                    if (this.savedata.ClueCount == 8)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Time.timeScale = 0;
                        this.EndScreen.SetActive(true);
                    }
                    else
                    {
                        this.ReturnScreen.SetActive(true);
                    }


            }

        }
    }

}   


