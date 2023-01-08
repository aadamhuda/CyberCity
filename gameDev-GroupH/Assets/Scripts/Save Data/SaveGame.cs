using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveGame : MonoBehaviour
{
    // Start is called before the first frame update
    private SphereCollider sc;

    [SerializeField]
    private DialogueManager dialgoue;

    private bool triggered;

    [SerializeField]
    private SaveData savedata;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private GameObject indicator;

    [SerializeField]
    private GameObject PauseMenu;

    [SerializeField]
    private ScriptDialogue script;

    void Start()
    {
        triggered = false;
        sc = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.triggered == false)
        {
            dialgoue.Script(savedata.get_save_index(), script);
            this.savedata.set_save_index(this.dialgoue.GetIndex());
            this.triggered = true;
        }
    }    
    


    private void Update()
    {
        if (player.get_save())
        {
            this.indicator.SetActive(true);
        }
        else
            this.indicator.SetActive(false);
    }

   void OnSave(InputValue value)
    {
        if (player.get_save())
        {
            if (value.isPressed)
            {
                Time.timeScale = 0; //freezes the game
                this.PauseMenu.SetActive(true); //pause menu appears

                //cursor is now visible 
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                this.PauseMenu.GetComponent<PauseController>().OpenSaveMenu();
            }
        }
    }
}
