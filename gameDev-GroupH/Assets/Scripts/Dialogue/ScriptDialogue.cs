using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDialogue : MonoBehaviour
{
    [SerializeField]
    private string [] dialogue = new string[] { };

    public string [] GetDialogue() { return this.dialogue; }
}
