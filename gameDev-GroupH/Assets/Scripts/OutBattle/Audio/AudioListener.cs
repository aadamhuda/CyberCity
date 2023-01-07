using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListener : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(mainCamera.forward, mainCamera.up);
    }
}
