using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SaveData saveState;
    public Player ply;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //clears all saved data variables
    public void initialiseGame()
    {
        Time.timeScale = 1.0f;
        saveState.Clue.Clear();
        saveState.Death.Clear();
        saveState.ClueCount = 0;
        saveState.SaveLocation((float)-115.4, 1, (float)-65.9);
        saveState.SavePlayerHealth(new float [] { ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP() });
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
