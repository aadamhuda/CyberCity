using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    //public Scene currentScene = SceneManager.GetActiveScene();
    //private string sceneName;
    public SaveData saveState;

    private void Start()
    {
        //cursor is now visible 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        //when p is pressed game is paused and the pause menu is loaded
        if (Input.GetKey("p"))
        {
            Time.timeScale = 0; //freezes the game
            _pauseMenu.SetActive(true); //pause menu appears

            //cursor is now visible 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //resumes game when resume button is pressed
    public void ResumeGame()
    {
        Time.timeScale = 1f; //unfreezes the game
        _pauseMenu.SetActive(false); //pause menu disappears
    }

    //quits to the main menu when the quit button is pressed
    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}