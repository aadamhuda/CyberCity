using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public string targetScene;
    public SaveData saveState;
    public GameObject cityScreen;
    public GameObject cargoScreen;
    // Start is called before the first frame update
    void Start()
    {
        //gets the current level, and then starts loading procedures
        targetScene = saveState.get_current_level();
        PickScreen();
        LoadScene();
    }
    //picks either cargo or city screen dependent on the level to be loaded
    void PickScreen()
    {
        if (targetScene == saveState.get_cyber_level())
        {
            cityScreen.SetActive(true);
            cargoScreen.SetActive(false);
        }
        else
        {
            cityScreen.SetActive(false);
            cargoScreen.SetActive(true);
        }
    }
    
    //loads the scene with short wait time
    void LoadScene()
    {
        Debug.Log(this.targetScene);
        SceneManager.LoadScene(targetScene);
    }
}
