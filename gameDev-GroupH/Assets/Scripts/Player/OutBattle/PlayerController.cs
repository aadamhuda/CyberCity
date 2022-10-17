using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public SaveData savedata;

    void Start()
    {
    /*    if (savedata == false)
        {
            transform.position = new Vector3(savedata.getX(), savedata.getY(), savedata.getZ());
        } */
    }

    // Moving player position with translation and roation
    void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float turnMovement = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * forwardMovement);
        transform.Rotate(Vector3.up * turnMovement);

        if (Input.GetKeyDown(KeyCode.G) ) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}