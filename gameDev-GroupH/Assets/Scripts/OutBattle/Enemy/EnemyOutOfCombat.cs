using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyOutOfCombat : EnemyCollider
{
    private bool dead = false;

    public float radius;
    [Range(0, 360)]
    public float angle;
    private NavMeshAgent agent;

    public LayerMask whatisGround, PlayerMask, WallMask;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public Vector3[] PatrolPoints;
    private int PatrolPointsCounter = 0;
    private bool PatrolPointsFoward_Backwards = false;


    //public Vector3[] Limit = new Vector3[2];
    [SerializeField]
    private bool WithInArea = false;

    public bool inSight;

    
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    protected Animator anim;
    protected virtual void Start()
    {
        // Disables marker
        this.canvas.SetActive(false);

        // Adds spherecollider setting values
        SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 3;
        sc.isTrigger = true;

        // Adds itself to dict 
        // If exists then checks if it was killed or not
        PosSave.AddToDict(PosSave.Death, gameObject.name);
        dead = PosSave.getDict(PosSave.Death, gameObject.name);

        // setting animators
        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);

        // If it was killed then remove itself
        if (dead == true)
            gameObject.SetActive(false);
        // LOS 
        else
            StartCoroutine(FOVRoutine());
    }

    public Animator GetAnim() { return this.anim; }

    public NavMeshAgent GetAgent() { return this.agent; }

    // Activate marker
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        this.canvas.SetActive(true);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        this.canvas.SetActive(false);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            // Update LOS every 0.5f;
            yield return wait;
            FieldOfViewCheck();
        }
    }

    protected virtual void FieldOfViewCheck()
    {
        // Create a sphere and checks if the player entered
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, PlayerMask);

        if (rangeChecks.Length != 0)
        {
            // If anything in sphere
            Transform target = rangeChecks[0].transform;

            // Find direction of collider
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Calculates whehter object is within eye-angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Check if any obstacles in in the way
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

    void ChasePlayer()
    {
        // Enemy runs to player when seen
        anim.SetBool("isRunning", true);
        agent.speed = 4 + this.PosSave.GetDifficulty() * 2 ;
        agent.SetDestination(player.transform.position);
    }

    private void ChaseInArea()
    {
        // Enemy only chases within territory
        if (!Physics.Raycast(player.transform.position, -transform.up, 3f, whatisGround))
        {
            WithInArea = false;
            anim.SetBool("isRunning", false);
        }
        else
            WithInArea = true;

    }

    private IEnumerator AmbushPlayer()
    {
        // Enemy engages combat
        anim.SetBool("isAttacking", true);
        walkPointSet = agent.SetDestination(gameObject.transform.position);
        player.GetComponent<PlayerController>().canMove = false;

        // SPin move :)
        StartCoroutine(Rotate(this.gameObject, 0.2f, player.transform.position));
        Transform sword = this.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        yield return new WaitForSeconds(0.8f);
        sword.gameObject.SetActive(true);

        // Buffs enemy dmg as they started combat
        yield return new WaitForSeconds(1.3f);
        PosSave.OnEnemyDouble();
        PosSave.SaveEnem(this.gameObject.name);
        base.BattleScene();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Define LOS
        radius = 9 + this.PosSave.GetDifficulty() * 3;
        angle = 60 + this.PosSave.GetDifficulty() * 30;

    }

    // Update is called once per frame
    void Update()
    {

        if (can_move)
        {
            // Patrol until player is in LOS
            ChaseInArea();
            if (inSight & WithInArea)
            {
                ChasePlayer();
                if (inSight & inRange)
                    StartCoroutine(AmbushPlayer());
            }

            else
                Patrol();
        }

    }

    void OnAttack(InputValue value)
    {
        this.EngageControl(value);
    }

    void Patrol()
    {
        anim.SetBool("isWalking", true);
        agent.speed = 2;

        // Patrol to point if point hasnt been found
        if (!walkPointSet)
            FindWalkRange();

        if (walkPointSet)
            walkPointSet = agent.SetDestination(walkPoint);

        Vector3 distanceCovered = transform.position - walkPoint;


        if (distanceCovered.magnitude < 1f)
            walkPointSet = false;
    }

    void FindWalkRange()
    {
        // Loops back and forth in patrol
        if (PatrolPointsFoward_Backwards == false)
            PatrolNumber(1, PatrolPoints.Length - 1);
        else
            PatrolNumber(-1, 0);

    }

    private void PatrolNumber(int increment, int size)
    {
        // Finds out when loop backwards or forward
        walkPoint = PatrolPoints[PatrolPointsCounter];
        PatrolPointsCounter += increment;
        walkPointSet = true;

        // When reaching end of patrol loop backwards and vice versa
        if (PatrolPointsCounter == size)
            PatrolPointsFoward_Backwards = !PatrolPointsFoward_Backwards;
    }
}
