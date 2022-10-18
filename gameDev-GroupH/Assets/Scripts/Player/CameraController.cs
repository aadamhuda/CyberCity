using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;


    // Initialize offset to initial position of camera
    void Start()
    {
        offset = transform.position;
    }

    // Late update updates after all Update() methods
    // Modify position of camera based on players rigidbody position and offset
    // After player has moved, camera follows
    void LateUpdate()
    {
        transform.position = player.GetComponent<Rigidbody>().position + offset;
    }
}
