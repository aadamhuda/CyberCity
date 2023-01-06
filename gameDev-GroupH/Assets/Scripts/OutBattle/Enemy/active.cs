using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active : MonoBehaviour
{
    // Set Active and deactive canvas
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void showCanvas(bool bol)
    {
        gameObject.SetActive(bol);
    }
}
