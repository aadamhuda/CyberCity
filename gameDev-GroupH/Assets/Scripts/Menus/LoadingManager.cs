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
        targetScene = saveState.get_current_level();
        PickScreen();
        StartCoroutine(LoadScene());
    }

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
    
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(targetScene);
    }
}
