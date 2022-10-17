using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public SaveData savedata;
    public void ReturnButton()
    {
        //savedata.SwitchBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
}   
