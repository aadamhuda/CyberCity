using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SaveData saveState;
    public void initialiseGame()
    {
        saveState.Death.Clear();
        saveState.SaveLocation((float)-115.4, 1, (float)-65.9);
    }
    public void PlayGame()
    {
        initialiseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
