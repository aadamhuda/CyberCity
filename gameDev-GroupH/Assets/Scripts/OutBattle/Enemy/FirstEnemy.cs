using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : EnemyOutOfCombat
{
    // Start is called before the first frame update
    [SerializeField]
    private bool triggered = false;

    [SerializeField]
    public DialogueManager dialogue;

    [SerializeField]
    private ScriptDialogue script;

    private void Awake()
    {
        this.radius = 10;
    }


    protected override void Update()
    {
        anim.SetBool("First", true);

        if (inRange == true)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Player engages combat
                StartCoroutine(PlayerEngage());
            }
        }
        Debug.Log(inSight);

        if (inSight) 
            if (triggered == false)
            {
                this.dialogue.PauseAll();
                this.dialogue.Script(0, script);
                triggered = true;

            }
    }
}
