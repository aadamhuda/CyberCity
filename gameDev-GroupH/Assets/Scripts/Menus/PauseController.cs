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

    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject main_pause_screen;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    Transform items;
    [SerializeField]
    GameObject SettingPage;

    private void Start()
    {

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SaveGame()
    {
        if (player.get_save())
        {
            saveState.set_respawn(player.transform.position);
            saveState.set_current_level(SceneManager.GetActiveScene().name);
            saveState.SaveGame();
        }
        else
        {
            Debug.LogError("Cannot save in this area.");
        }
    }

    public void LoadGame()
    {
        if (player.get_save())
        {
            saveState.LoadData();
            SceneManager.LoadScene(saveState.get_current_level());
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("Cannot load in this area.");
        }
    }

    public void Inventory()
    {
        inventory.SetActive(true);
        main_pause_screen.SetActive(false);
        inventory.GetComponent<InventoryController>().LoadMenu();
        
    }

    public void quit_inventory()
    {
        inventory.SetActive(false);
        for (int i = 0; i < items.childCount; i++)
            Destroy(items.GetChild(i).gameObject);
        main_pause_screen.SetActive(true);
    }

    public void LoadSettings()
    {
        this.SettingPage.SetActive(true);
        this.main_pause_screen.SetActive(false);
    }    
    
    public void QuitSettings()
    {
        this.SettingPage.SetActive(false);
        this.main_pause_screen.SetActive(true);
    }

    //quits to the main menu when the quit button is pressed
    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}