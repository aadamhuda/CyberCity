using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Movement
    private Rigidbody rb;
    private  Vector2 moveValue;
    public float maxAngleChange;
    public float speed;
    public TextMeshProUGUI winText;
    public bool won = false;

    public SaveData savedata;

    void Start() {
        rb = GetComponent<Rigidbody>();
        winText.text = "";


        if (savedata.isNextScene == true)
        {
            transform.position = new Vector3(savedata.getX(), savedata.getY(), savedata.getZ());
            savedata.SwitchBool();
        }

    }

    // Input action
    // Can move with multiple control schemes, e.g. keyboard, controller etc
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Clue object dissappears when picked up and player wins 
        if(other.gameObject.tag == "Clue")
        {
            other.gameObject.SetActive(false);
            winText.text = "You Win! Press X to quit";
            won = true;
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        //Moving player position with translation and rotation based on input(old)
        //float forwardMovement = moveValue.y * speed * Time.deltaTime;
        //float turnMovement = moveValue.x * turnSpeed * Time.deltaTime;

        //transform.Translate(Vector3.forward * forwardMovement);
        //transform.Rotate(Vector3.up * turnMovement);

        //if user has won and they can press x to quit
        if (Input.GetKey("x") && won)
        {
            Debug.Log("Quit");
            Application.Quit();
        }

    }

    //Rigidbody movement of player, and rotation, using quaternion
    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        if (movement == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxAngleChange * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }
}