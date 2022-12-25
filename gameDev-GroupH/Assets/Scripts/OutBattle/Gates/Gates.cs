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
        foreach (GameObject gate in this.gates)
        {
            if (savedata.ClueCount >= gate.GetComponent<Gate_Info>().get_condition())
            {
                Debug.Log("eh eh ehe heh eh: ");
                Animator anim = gate.GetComponent<Animator>();
                anim.SetBool("ImmediateOpen", true);
            }

        }
    }
    public void CheckGates(int count)
    {
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
