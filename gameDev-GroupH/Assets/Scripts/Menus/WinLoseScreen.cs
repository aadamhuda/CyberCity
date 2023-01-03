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


    public void ActivateWin()
    {
        this.WinScreen.SetActive(true);
    }    
    
    public void ActivateLose()
    {
        this.LoseScreen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ReturnToLastSave()
    {
        this.savedata.LoadData(this.savedata.LastSave);
        SceneManager.LoadScene(this.savedata.get_current_level());
    }

    public void ReturnToLastLocation()
    {
        SceneManager.LoadScene(this.savedata.get_current_level());
    }
}
