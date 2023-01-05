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

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (File.Exists(Application.persistentDataPath + "/" + saveState.LastSave + ".test"))
            this.ContinueObject.SetActive(true);
        else
            this.NewObject.SetActive(true);
    }

    //clears all saved data variables
    public void initialiseGame()
    {
        PlayerPrefs.SetFloat("EffectsVolumePreference", 1f);
        Time.timeScale = 1.0f;
        saveState.Clue.Clear();
        saveState.Death.Clear();
        saveState.ClueCount = 0;
        saveState.set_current_level(saveState.get_cargo_level());
        saveState.SaveLocation(new Vector3(0f, 0.5f, 0f));
        saveState.set_respawn(new Vector3(0f, 0.5f, 0f));
        saveState.set_item_respawn(4);
        saveState.SavePlayerMP(new int[] { 30, 30, 30, 30 });
        saveState.set_dialogue_index(0);
        saveState.set_save_index(0);
        saveState.SavePlayerHealth(new int[] { ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP(), ply.getTotalMaxHP() });
    }

    //starts the game when play button is pressed
    public void PlayGame()
    {
        initialiseGame(); //clears saved data variables
        SceneManager.LoadScene("Loading");
    }

    //quits the game when the quit button is pressed
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/"+saveState.LastSave+".test"))
        {
            saveState.LoadData(saveState.LastSave);
            SceneManager.LoadScene("Loading");
        }

    }
}

