using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    bool cybercity = false;

    [SerializeField]
    private SaveData savedata;

    [SerializeField]
    private GameObject EndScreen;

    [SerializeField]
    private GameObject ReturnScreen;

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
        this.ReturnScreen.SetActive(false);
    }

    private void Update()
    {
        if (this.cybercity)
        {

            if (Input.GetMouseButtonDown(0))
            {
                    if (this.savedata.ClueCount == 8)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Time.timeScale = 0;
                        this.EndScreen.SetActive(true);
                    }
                    else
                    {
                        this.ReturnScreen.SetActive(true);
                    }


            }

        }
    }

}   


