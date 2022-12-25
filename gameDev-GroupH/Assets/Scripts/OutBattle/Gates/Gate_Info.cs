using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Info : MonoBehaviour
{
    [SerializeField]
    private int condition;

    public int get_condition() { return this.condition; }
}
