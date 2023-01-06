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
    public GameObject errorMsg;
    public float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //gets the current level, and then starts loading procedures
        targetScene = saveState.get_current_level();
        PickScreen();
        LoadLevel();
    }
    
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 10f)
        {
            StartCoroutine(ErrorMessage());
            SceneManager.LoadScene("Main Menu");
        }
    }
    IEnumerator ErrorMessage()
    {
        errorMsg.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
    //picks either cargo or city screen dependent on the level to be loaded
    void PickScreen()
    {
        Debug.Log(targetScene);
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

        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        
        yield return new WaitForSeconds(3f);
    }

    //loads the scene with short wait time
    void LoadLevel()
    {
        Debug.Log(targetScene);

        
        SceneManager.LoadScene(targetScene);
        
    }
}
