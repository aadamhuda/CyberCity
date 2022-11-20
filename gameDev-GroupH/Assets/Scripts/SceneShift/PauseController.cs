using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    public SaveData saveState;

    private void Update()
    {
        //when p is pressed game is paused and the pause menu is loaded
        if (Input.GetKey("p"))
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true); //pause menu appears

            //cursor is now visible 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 0.1f;
        _pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}