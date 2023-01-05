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
    }

    public void OpenSaveMenu()
    {
        this.main_pause_screen.SetActive(false);
        this.SaveMenu.LoadMenu();
    }

    public void CloseSaveMenu()
    {
        this.SaveMenu.Disable();
        Debug.Log("bruh");
        this.main_pause_screen.SetActive(true);
    }

    public void SaveGame(string SaveName)
    {
        if (player.get_save())
        {
            this.SaveLoadNotification.Activate("Saving Data", this.SaveMenu.gameObject);
            saveState.set_respawn(player.transform.position);
            saveState.set_current_level(SceneManager.GetActiveScene().name);
            saveState.SaveGame(SaveName);
            this.SaveLoadNotification.ChangeText("Saved");
        }
        else
        {
            this.SaveLoadNotification.Activate("Must save near checkpoint", this.SaveMenu.gameObject);
        }
    }

    public void LoadGame(string PathName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + PathName + ".test"))
        {
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

    public void OpenLoadMenu()
    {
        this.main_pause_screen.SetActive(false);
        this.LoadMenu.LoadMenu();
        
    }    
    
    public void CloseLoadMenu()
    {
        this.LoadMenu.Disable();
        this.main_pause_screen.SetActive(true);
    }

    //quits to the main menu when the quit button is pressed
    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}