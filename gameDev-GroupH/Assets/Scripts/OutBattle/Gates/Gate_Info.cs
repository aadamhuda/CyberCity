using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Info : MonoBehaviour
{
    [SerializeField]
    private int condition;
    // Condition for gate to open
    // Amount of clues needed to open
    public int get_condition() { return this.condition; }
}
