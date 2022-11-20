using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public SaveData saveState;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    ////clears all saved data variables
    //public void initialiseGame()
    //{
    //    Time.timeScale = 1.0f;
    //    saveState.Clue.Clear();
    //    saveState.Death.Clear();
    //    saveState.ClueCount = 0;
    //    saveState.SaveLocation((float)-115.4, 1, (float)-65.9);
    //}

    //loads gameplay from battle
    public void intialiseBattle()
    {
        Time.timeScale = 1.0f;

    }

    public void LoadGame()
    {
        if (saveState.inBattle == true)
        {
            intialiseBattle();
            SceneManager.LoadScene("Battle");
        } else {
            SceneManager.LoadScene("Prototype");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
