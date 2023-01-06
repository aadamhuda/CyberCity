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
        this.canvas.SetActive(false);

        SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 3;
        sc.isTrigger = true;
        PosSave.AddToDict(PosSave.Death, gameObject.name);
        dead = PosSave.getDict(PosSave.Death, gameObject.name);

        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);

        if (dead == true)
            gameObject.SetActive(false);
        else
            StartCoroutine(FOVRoutine());
    }

    public Animator GetAnim() { return this.anim; }

    public NavMeshAgent GetAgent() { return this.agent; }

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
            yield return wait;
            FieldOfViewCheck();
        }
    }

    protected virtual void FieldOfViewCheck()
    {
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

    void ChasePlayer()
    {
        anim.SetBool("isRunning", true);
        agent.speed = 4 + this.PosSave.GetDifficulty() * 2 ;
        agent.SetDestination(player.transform.position);
    }

    private void ChaseInArea()
    {

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
        anim.SetBool("isAttacking", true);
        walkPointSet = agent.SetDestination(gameObject.transform.position);
        player.GetComponent<PlayerController>().canMove = false;

        StartCoroutine(Rotate(this.gameObject, 0.2f, player.transform.position));
        Transform sword = this.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        yield return new WaitForSeconds(0.8f);
        sword.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.3f);
        PosSave.OnEnemyDouble();
        base.BattleScene();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        radius = 9 + this.PosSave.GetDifficulty() * 3;
        angle = 60 + this.PosSave.GetDifficulty() * 30;

    }

    // Update is called once per frame
    void Update()
    {

        if (can_move)
        {
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
        if (PatrolPointsFoward_Backwards == false)
            PatrolNumber(1, PatrolPoints.Length - 1);
        else
            PatrolNumber(-1, 0);

    }

    private void PatrolNumber(int increment, int size)
    {
        walkPoint = PatrolPoints[PatrolPointsCounter];
        PatrolPointsCounter += increment;
        walkPointSet = true;
        if (PatrolPointsCounter == size)
            PatrolPointsFoward_Backwards = !PatrolPointsFoward_Backwards;
    }
}
