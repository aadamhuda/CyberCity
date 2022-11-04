using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private Transform mainCamera;

    [SerializeField]
    private Vector2 moveValue;

    [SerializeField]
    private float walkSpeed = 7f;

    [SerializeField]
    private float runSpeed = 15f;

    [SerializeField]
    private float rotationSpeed = 7f;

    [SerializeField]
    private float jumpHeight = 1f;

    [SerializeField]
    private float gravityValue = -15f;

    private float speed;

    public TextMeshProUGUI winText;
    public bool won = false;
    public int numClues = 3;
    public int clueCount;

    public SaveData savedata;

    void Start()
    {
        speed = walkSpeed;

        controller = GetComponent<CharacterController>();
        winText.text = "";
        clueCount = 0;

        if (savedata.isNextScene == true)
        {
            transform.position = new Vector3(savedata.getX(), savedata.getY(), savedata.getZ());
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
        if (other.gameObject.tag == "Clue")
        {
            other.gameObject.SetActive(false);
            clueCount++;
            checkPlayerWin();
        }
    }

    void checkPlayerWin()
    {
        if (clueCount >= numClues)
        {
            winText.text = "You Win! Press X to quit";
            won = true;
            Time.timeScale = 0;
            savedata.SwitchBool();
        }
    }

    // Player movement
    void Update()
    {
        checkGrounded();
        Move();
        Jump();

        //if user has won and they can press x to quit
        if (Input.GetKey("x") && won)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
    void checkGrounded()
    {
        //check if player is grounded
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    void Move()
    {
        // Movement based on player input direction and camera direction 
        Vector3 movement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * new Vector3(moveValue.x, 0, moveValue.y).normalized;

        // If you are on the ground and not holding shift, walk
        if (groundedPlayer && !Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }
        // If you are holding shift and you are on the ground, run
        if (Input.GetKey(KeyCode.LeftShift) && groundedPlayer)
        {
            speed = runSpeed;
        }
        // If you are not running anymore, walk if you are on the ground otherwise if you are mid air, continue running
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (groundedPlayer)
            {
                speed = walkSpeed;
            }
            else
            {
                speed = runSpeed;
            }
        }
        controller.Move(movement * speed * Time.deltaTime);

        // Rotation
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void Jump()
    {
        // Changes the height position of the player..
        if (Input.GetAxisRaw("Jump") == 1 && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Jump movement
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
