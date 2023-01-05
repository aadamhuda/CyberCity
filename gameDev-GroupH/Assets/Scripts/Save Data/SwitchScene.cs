using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    bool cybercity = false;

    [SerializeField]
    private SaveData savedata;
    public void ReturnButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void OnTriggerEnter(Collider collision)
    {
        this.cybercity = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        this.cybercity = false;
    }

    private void Update()
    {
        if (this.cybercity)
        {
            Debug.Log(savedata.get_cyber_level());
            if (Input.GetMouseButtonDown(0))
            {

                savedata.SaveLocation(new Vector3(0, 1.5f, 0));

                savedata.set_current_level(savedata.get_cyber_level());

                SceneManager.LoadScene("Loading");
            }
        }
    }

}