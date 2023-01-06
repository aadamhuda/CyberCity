using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.IO;

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
    LoadController LoadMenu;
    [SerializeField]
    SaveController SaveMenu;
    [SerializeField]
    SaveANDLoad SaveLoadNotification;

    public SettingsMenu settings;

    private void Start()
    {
        settings.LoadSettings();
    }

    void OnPause(InputValue value)
    {
        //when p is pressed game is paused and the pause menu is loaded
        if (value.isPressed)
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
        settings.SaveSettings();
    }

    public void OpenSaveMenu()
    {
        // Opens save options
        this.main_pause_screen.SetActive(false);
        this.SaveMenu.LoadMenu();
    }

    public void CloseSaveMenu()
    {
        // Returns to main menu
        this.SaveMenu.Disable();
        this.main_pause_screen.SetActive(true);
    }

    public void SaveGame(string SaveName)
    {
        // If player is within checkpoint area
        if (player.get_save())
        {
            // Start a notification
            this.SaveLoadNotification.Activate("Saving Data", this.SaveMenu.gameObject);
            // Saves players positions
            saveState.set_respawn(player.transform.position);
            // SAves current scene
            saveState.set_current_level(SceneManager.GetActiveScene().name);
            // Saves file saved
            saveState.SaveGame(SaveName);
            this.SaveLoadNotification.ChangeText("Saved");
        }
        else
        {
            // tells the user to be in range
            this.SaveLoadNotification.Activate("Must save near checkpoint", this.SaveMenu.gameObject);
        }
    }

    public void LoadGame(string PathName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + PathName + ".test"))
        {
            // Checks if load data exists
            saveState.LoadData(PathName);
            SceneManager.LoadScene(saveState.get_current_level());

            Time.timeScale = 1f;
        }
        else
        {
            this.SaveLoadNotification.Activate("No data saved here", this.LoadMenu.gameObject);
        }

    }

    public void Inventory()
    {
        // Opens inventory
        inventory.SetActive(true);
        main_pause_screen.SetActive(false);
        inventory.GetComponent<InventoryController>().LoadMenu();
        
    }

    public void quit_inventory()
    {
        // Returns to main menu
        inventory.SetActive(false);
        for (int i = 0; i < items.childCount; i++)
            Destroy(items.GetChild(i).gameObject);
        main_pause_screen.SetActive(true);
    }

    public void OpenLoadMenu()
    {
        // Open load menu
        this.main_pause_screen.SetActive(false);
        this.LoadMenu.LoadMenu();
        
    }    
    
    public void CloseLoadMenu()
    {
        // Closes load menu
        this.LoadMenu.Disable();
        this.main_pause_screen.SetActive(true);
    }

    //quits to the main menu when the quit button is pressed
    public void QuitGame()
    {
        // Quits to main menu
        SceneManager.LoadScene("Main Menu");
    }
}