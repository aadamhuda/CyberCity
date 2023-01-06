using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MenuController : MonoBehaviour
{
    public SaveData saveState;
    public Player ply;

    [SerializeField]
    private GameObject NewObject;

    [SerializeField]
    private GameObject ContinueObject;

    public GameObject buttons;

    private void Start()
    {
        // Visible cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Diaplys load game if last save exist
        if (File.Exists(Application.persistentDataPath + "/" + saveState.LastSave + ".test"))
            this.ContinueObject.SetActive(true);
        else
            this.NewObject.SetActive(true);
    }

    //clears all saved data variables
    public void initialiseGame()
    {
        // Setting values for beginning of game
        PlayerPrefs.SetFloat("EffectsVolumePreference", 1f);
        Time.timeScale = 1.0f;
        saveState.Clue.Clear();
        saveState.Death.Clear();
        saveState.ClueCount = 0;
        saveState.set_current_level(saveState.get_cargo_level());
        saveState.SaveLocation(new Vector3(0f, 1.5f, 0f));
        saveState.set_respawn(new Vector3(0f, 1.5f, 0f));
        //changes item respawn for difficulty
        saveState.set_item_respawn(5 - saveState.GetDifficulty());
        saveState.SavePlayerMP(new int[] { 30, 30, 30, 30 });
        saveState.set_dialogue_index(0);
        saveState.set_save_index(0);
        saveState.ChangeRespawn();
        saveState.SavePlayerHealth(new int[] { ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP() });
    }

    // Setting difficulty
    public void SetEasy()
    {
        saveState.SetDifficulty(1);
        PlayGame();
    }
    public void SetNormal()
    {
        saveState.SetDifficulty(2);
        PlayGame();
    }
    public void SetHard()
    {
        saveState.SetDifficulty(3);
        PlayGame();
    }

    public void SelectDifficulty()
    {
        buttons.SetActive(true);
    }
    //starts the game when play button is pressed
    public void PlayGame()
    {
        // Load fresh game
        initialiseGame(); //clears saved data variables
        SceneManager.LoadScene("Loading");

    }


    //quits the game when the quit button is pressed
    public void LoadGame()
    {
        // Loads previously saved game
        if (File.Exists(Application.persistentDataPath + "/"+saveState.LastSave+".test"))
        {
            saveState.LoadData(saveState.LastSave);
            SceneManager.LoadScene("Loading");
        }

    }
}

