using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    void Update()
    {
        anim.SetBool("First", true);
        if (inSight) 
            if (triggered == false)
            {
                this.dialogue.PauseAll();
                this.dialogue.Script(0, script);
                triggered = true;
            }
    }

    void OnAttack(InputValue value)
    {
        if (inRange == true)
        {
            if (value.isPressed)
            {
                tutorial = true;
                // Player engages combat
                StartCoroutine(PlayerEngage());
            }
        }
    }
}
