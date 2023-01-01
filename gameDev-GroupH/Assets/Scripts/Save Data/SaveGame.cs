using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        triggered = false;
        sc = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.triggered == false)
        {
            dialgoue.Script(savedata.get_save_index(), "/Scripts/Dialogue/save.txt", "save");
            this.triggered = true;
        }
    }    
    


    private void Update()
    {
        if (player.get_save())
        {
            this.indicator.SetActive(true);
            if (Input.GetKeyDown(KeyCode.S))
            {
                savedata.SaveGame();
            }

        }
        else
            this.indicator.SetActive(false);
    }
}
