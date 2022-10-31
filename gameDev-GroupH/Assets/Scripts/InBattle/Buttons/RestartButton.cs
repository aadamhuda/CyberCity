using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    public GameObject restartButton;
    public Player player;

    
    // Start is called before the first frame update
    void Start()
    {
        restartButton = GameObject.FindGameObjectWithTag("Restart");
        disableButton(restartButton);

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // function to enable 
    public void enableButton(GameObject button)
    {
        Debug.Log("enabling");
        button.SetActive(true);
    }

    // function to disable
    public void disableButton(GameObject button)
    {
        button.SetActive(false);
    }
}
