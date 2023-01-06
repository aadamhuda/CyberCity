using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    // How many clues left
    [SerializeField]
    private int open_condition;

    // Gates
    [SerializeField]
    private GameObject[] gates;

    [SerializeField]
    private SaveData savedata;

    private void Start()
    {
        // Chceck if the player already met requirements for gate to open
        // If so, then the agte must be open
        foreach (GameObject gate in this.gates)
        {
            if (savedata.ClueCount >= gate.GetComponent<Gate_Info>().get_condition())
            {
                Animator anim = gate.GetComponent<Animator>();
                anim.SetBool("ImmediateOpen", true);
            }

        }
    }
    public void CheckGates(int count)
    {
        // If enought clues are colleted the gate will open
        foreach (GameObject gate in this.gates)
        {
            if (count >= gate.GetComponent<Gate_Info>().get_condition())
            {
                Animator anim = gate.GetComponent<Animator>();
                anim.SetBool("Open", true);
            }

        }

    }


}
