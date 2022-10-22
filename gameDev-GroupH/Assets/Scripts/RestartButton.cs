using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{

    public GameObject restartButton;

    
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

    // function to enable 
    public void enableButton(GameObject button)
    {
        button.SetActive(true);
    }

    // function to disable
    public void disableButton(GameObject button)
    {
        button.SetActive(false);
    }
}
