using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private LayerMask safe_area;

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

    private Animator animator;
    private int jumpHash;

    public bool canMove = true;

    public GameObject [] Clues = new GameObject[3];

    public TextMeshProUGUI winText;
    public TextMeshProUGUI interaction;
    [SerializeField]
    private TextMeshProUGUI saveText;
    public bool won = false;
    public int numClues = 3;
    public int clueCount;

    public SaveData savedata;

    bool collecterBool = false;
    Collider collecter;

    void Start()
    {
        var move = gameObject.GetComponent<Transform>();

        if (savedata.getRespawn())
        {
            move.position = savedata.GetLocation();
            savedata.ChangeRespawn();
        }
            
        else
            move.position = new Vector3(savedata.getX(), savedata.getY(), savedata.getZ());


        canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        jumpHash = Animator.StringToHash("Jump");
        winText.text = "";
        interaction.text = "";
        clueCount = savedata.ClueCount;
    }

    // Input action
    // Can move with multiple control schemes, e.g. keyboard, controller etc
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();

    }

    public void collectClue(Collider other)
    {
        other.gameObject.SetActive(false);
        interaction.text = "";
        clueCount++;
        savedata.ClueCount = clueCount;
        checkPlayerWin();
        collecterBool = false;
        collecter = null;
        savedata.DictBoolSwitch(savedata.Clue, other.name);
    }

    void OnTriggerEnter(Collider other)
    {
        // Clue object dissappears when picked up and player wins 
        foreach (GameObject obj in Clues)
        {
            if (obj.name == other.name)
            {
                interaction.text = "Press k to interact";
                collecter = other;
                collecterBool = true;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        interaction.text = "";

    }

    //checks if the player has collected all the clues
    void checkPlayerWin()
    {
        if (clueCount >= numClues)
        {
            winText.text = "You Win! Press X to go to the main menu";
            won = true;
            Time.timeScale = 0;
            savedata.SwitchBool();
        }
    }

    // Player movement
    void Update()
    {
        checkGrounded();
        if (canMove)
        {
            Move();
            Jump();
        }
        else
        {
            controller.Move(new Vector3(0f,0f,0f));
            animator.SetFloat("Speed", 0, 0.2f, Time.deltaTime);
        }


        //if user has won and they can press x to quit
        if (Input.GetKey("x") && won)
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (collecterBool == true)
        {
            if (Input.GetKey(KeyCode.K))
            {
                collectClue(collecter);
            }
        }

        if (Physics.Raycast(gameObject.transform.position, -transform.up, 3f, safe_area))
        {
            saveText.text = "Press 'm' to save";
            if (Input.GetKey("m"))
            {
                Debug.Log("This WOMAN is spending 3k on a PC");
                savedata.Set_respawn(gameObject.transform.position);
                
            }
        }
        else
            saveText.text = "";
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

        float currentSpeed = Mathf.Clamp01(Mathf.Abs(moveValue.x) + Mathf.Abs(moveValue.y));

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

        // Set animation based on current speed
        // Idle
        if (currentSpeed == 0)
        {
            animator.SetFloat("Speed", 0, 0.2f, Time.deltaTime);
        }
        else
        {
            // Jog
            if (speed == walkSpeed)
            {
                animator.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
            }
            // Sprint
            else
            {
                animator.SetFloat("Speed", 1, 0.2f, Time.deltaTime);
            }
        }
    }
    void Jump()
    {
        // Changes the height position of the player..
        if (Input.GetAxisRaw("Jump") == 1 && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

            // Jump animation
            animator.Play(jumpHash);
        }

        // Jump movement
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
