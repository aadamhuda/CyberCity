using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField]
    SaveData savedata;

    [SerializeField]
    private GameObject WinScreen;

    [SerializeField]
    private GameObject LoseScreen;

    // Actiavtes win or lose screen
    public void ActivateWin()
    {
        this.WinScreen.SetActive(true);
    }    
    
    public void ActivateLose()
    {
        this.LoseScreen.SetActive(true);
    }

    // Buttons to return to menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // buttons to return last save
    public void ReturnToLastSave()
    {
        this.savedata.LoadData(this.savedata.LastSave);
        SceneManager.LoadScene(this.savedata.get_current_level());
    }

    // Return to last saved location
    public void ReturnToLastLocation()
    {
        SceneManager.LoadScene(this.savedata.get_current_level());
    }
}
