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
    private float walkSpeed = 7f;

    [SerializeField]
    private float runSpeed = 15f;

    [SerializeField]
    private float rotationSpeed = 7f;

    [SerializeField]
    private float jumpHeight = 1f;

    [SerializeField]
    private float gravityValue = -15f;

    [SerializeField]
    private DialogueManager dialogue;

    public InputAction interactionControls;

    //Movement
    private Vector2 moveValue;
    private bool isRunning = false;
    private float speed;
    private Animator animator;

    public bool canMove = true;

    public GameObject[] Clues = new GameObject[4];

    public TextMeshProUGUI winText;
    [SerializeField]
    private bool save;
    public bool won = false;
    public int numClues = 4;
    public int clueCount;

    public SaveData savedata;

    bool collecterBool = false;
    Collider collecter;

    [SerializeField]
    Gates gate;

    void Start()
    {
        var move = gameObject.GetComponent<Transform>();

        if (savedata.getRespawn())
        {
            move.position = savedata.get_respawn_location();
            savedata.ChangeRespawn();
        }

        else
            move.position = savedata.get_player_location();


        canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        winText.text = "";
        clueCount = savedata.ClueCount;

    }

    // Input action controls
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        if (value.isPressed)
        {
            // On Button Down
            isRunning = true;
        }
        else 
        {
            // On Button Up
            isRunning = false;
        }
    }

    void OnCollect(InputValue value)
    {
        if (collecterBool == true)
        {
            if (value.isPressed)
            {
                collectClue(collecter);
            }
        }
    }


    public bool get_save() { return this.save; }

    public void collectClue(Collider other)
    {
        other.gameObject.SetActive(false);

        clueCount++;
        savedata.ClueCount = clueCount;

        gate.CheckGates(clueCount);
        collecterBool = false;
        collecter = null;
        savedata.DictBoolSwitch(savedata.Clue, other.name);

        this.dialogue.PauseAll();
        this.dialogue.Script(savedata.get_clues_text_index(), "/Scripts/Dialogue/clues.txt", "clue");
    }

    void OnTriggerEnter(Collider other)
    {
        // Clue object dissappears when picked up and player wins 
        foreach (GameObject obj in Clues)
        {
            if (obj.name == other.name)
            {
                collecter = other;
                collecterBool = true;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        collecterBool = false;
    }

    //checks if the player has collected all the clues

    // Player movement
    void Update()
    {
        checkGrounded();
        if (canMove)
        {
            Move();
            Gravity();
        }
        else
        {
            controller.Move(new Vector3(0f, 0f, 0f));
            animator.SetFloat("Speed", 0, 0.2f, Time.deltaTime);
        }


        //if user has won and they can press x to quit
        if (Input.GetKey("x") && won)
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Physics.Raycast(gameObject.transform.position, -transform.up, 3f, safe_area))
            save = true;
        else
            save = false;
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
        float horizontalInput = moveValue.x;
        float verticalInput = moveValue.y;

        Vector3 movement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput).normalized;

        float currentSpeed = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        // If you are on the ground and not holding shift, walk
        if (groundedPlayer && (!isRunning))
        {
            speed = walkSpeed;
        }
        // If you are holding shift and you are on the ground, run
        if (isRunning && groundedPlayer)
        {
            speed = runSpeed;
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
    void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
