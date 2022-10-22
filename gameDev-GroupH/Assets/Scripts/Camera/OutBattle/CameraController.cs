using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform freeLook;
    public float rotationSpeed;
    float mouseX, mouseY;


    // Initialize offset to initial position of camera
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Adjust freelook based on mouse position (X, Y) and rotation with eulerAngles
    // If right mouse is clicked, freelook
    void LateUpdate()
    {
        // Getting the rotations
        float rotationX;
        float rotationY = freeLook.eulerAngles.y;
        if (freeLook.eulerAngles.x <= 180f)
        {
            rotationX = freeLook.eulerAngles.x;
        }
        else
        {
            rotationX = freeLook.eulerAngles.x - 360f;
        }

        float minRotation = -40;
        float maxRotation = 25;
        // Clamping the X rotation (moving camera up and down)
        // -40 min to stop camera going under floor
        // 25 max to stop camera going over player
        if (Input.GetMouseButton(1)){
            mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
            if(rotationX <= -40)
            {
                freeLook.transform.rotation = Quaternion.Euler(minRotation, rotationY, 0);
            }
            if (rotationX >= 25) {
                freeLook.transform.rotation = Quaternion.Euler(maxRotation, rotationY, 0);
            }
                freeLook.eulerAngles += new Vector3(-mouseY, mouseX, 0);
        }   

    }
}
