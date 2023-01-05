using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportScript : MonoBehaviour
{
    [SerializeField]
    private ScriptDialogue script;

    [SerializeField]
    private DialogueManager dialogue;

    [SerializeField]
    private SphereCollider sphere;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered == false)
        {
            this.dialogue.Script(0, script);
            this.triggered = true;
        }
    }
}
