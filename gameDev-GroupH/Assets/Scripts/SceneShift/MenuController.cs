using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SaveData saveState;

    private void Start()
    {
        Cursor.visible = true;
    }

    //clears all saved data variables
    public void initialiseGame()
    {
        saveState.Clue.Clear();
        saveState.Death.Clear();
        saveState.ClueCount = 0;
        saveState.SaveLocation((float)-115.4, 1, (float)-65.9);
    }
    public void PlayGame()
    {
        initialiseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
