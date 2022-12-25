using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOutOfCombat : EnemyCollider
{
    private bool dead = false;
    public float radius;
    [Range(0, 360)]
    public float angle;
    public NavMeshAgent agent;

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
    private Animator anim;
    protected void Start()
    {
        engage.text = "";
        SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 5;
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

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
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
        agent.speed = 10;
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
        yield return new WaitForSeconds(2f);
        PosSave.OnEnemyDouble();
        base.BattleScene();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        radius = 15;
        angle = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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
