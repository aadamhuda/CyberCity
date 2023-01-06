using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FirstEnemy : MonoBehaviour
{
    private bool dead;    

    [SerializeField]
    private bool triggered = false;

    [SerializeField]
    public DialogueManager dialogue;

    [SerializeField]
    private ScriptDialogue script;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private SaveData savedata;

    [SerializeField]
    private float radius;

    [SerializeField]
    [Range(0, 360)]
    public float angle;

    [SerializeField]
    private LayerMask  PlayerMask, WallMask;

    [SerializeField]
    private bool inSight;

    private bool inRange = false;

    private bool tutorial = true;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private Animator anim;

    private void Start()
    {
        // Setting LOS
        this.radius = 10;
        this.angle = 360;

        // Checks if the enemy is already dead

        this.savedata.AddToDict(this.savedata.Death, gameObject.name);
        dead = this.savedata.getDict(this.savedata.Death, gameObject.name);

        if (dead == true)
            gameObject.SetActive(false);
        else
            StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            // Check for LOS
            yield return wait;
            FieldOfViewCheck();
        }
    }

    public float GetRadius() { return this.radius; }
    
    public float GetAmgle() { return this.radius; }
    public bool GetInRange() { return this.inRange; }
    public GameObject GetPlayer() { return this.player; }

    // CHecks if player has entered collider

    void OnTriggerEnter(Collider Other)
    {
        this.inRange = true;
        this.canvas.SetActive(true);
    }

    protected void OnTriggerExit(Collider Other)
    {
        this.inRange = false;
        this.canvas.SetActive(false);
    }

    void FieldOfViewCheck()
    {
        // C & P from EnemyOutOfCombat
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, PlayerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, WallMask))
                    inSight = true;
                else
                    inSight = false;
            }
            else
                inSight = false;
        }
        else if (inSight)
            inSight = false;
    }

    protected void BattleScene()
    {
        // Engage combat
        savedata.tutorial = tutorial;
        savedata.SaveLocation(player.transform.position);
        savedata.set_current_level(SceneManager.GetActiveScene().name);
        savedata.inBattle = true;
        SceneManager.LoadScene("Battle");
    }

    public void EngageControl(InputValue value)
    {
        if (inRange == true)
        {

            if (value.isPressed)
            {
                // Player engages combat
                StartCoroutine(PlayerEngage());
            }
        }
    }

    public IEnumerator Rotate(GameObject o, float speed, Vector3 targetPos)
    {
        // Spin move :)
        var transform = o.transform;
        var startRotation = transform.rotation;
        var direction = targetPos - transform.position;
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0;
        targetRotation.z = 0;
        var t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime / speed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    public IEnumerator PlayerEngage()
    {
        // C & P from EnemyCollider
        Transform t = player.transform;
        Animator a = player.GetComponent<Animator>();
        Transform sword = t.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        StartCoroutine(Rotate(player, 0.2f, gameObject.transform.position));
        player.GetComponent<PlayerController>().canMove = false;

        a.CrossFade("EquipSword", 0.1f);
        yield return new WaitForSeconds(0.5f);

        sword.gameObject.SetActive(true);
        a.CrossFade("Melee", 0.1f);
        yield return new WaitForSeconds(1f);

        savedata.SaveEnem(gameObject.name);
        BattleScene();
    }

    void OnAttack(InputValue value)
    {
        Debug.Log(value);
        this.EngageControl(value);
    }

    void Update()
    {

        if (inSight)
        {
            if (triggered == false)
            {
                // Only trigger once and play dialogue
                this.dialogue.PauseAll();
                this.dialogue.Script(0, script);
                triggered = true;
            }
        }

    }


}
