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
<<<<<<< Updated upstream
=======

    [SerializeField]
    private Animator anim;
>>>>>>> Stashed changes
    protected void Start()
    {
        SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 5;
        sc.isTrigger = true;
        PosSave.AddToDict(PosSave.Death, gameObject.name);
        dead = PosSave.getDict(PosSave.Death, gameObject.name);
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
        Debug.Log("Chasing");
        agent.SetDestination(player.transform.position);
    }

<<<<<<< Updated upstream
=======
    private void ChaseInArea ()
    {
            
        if (!Physics.Raycast(player.transform.position, -transform.up, 3f, whatisGround))
        {
            WithInArea = false;
            anim.SetBool("isRunning", false);
            Debug.Log("Santa Claus ate my primos");
        }
        else
            WithInArea = true;
            
        Debug.Log("Felix is : " + WithInArea);
    }

    private IEnumerator AmbushPlayer()
    {
        
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(2f);
        PosSave.OnEnemyDouble();
        base.BattleScene();
    }
>>>>>>> Stashed changes
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        radius = 100;
        angle = 360;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        ChaseInArea();
        if (inSight & WithInArea)
        {
<<<<<<< Updated upstream
            ChasePlayer();
            if (inSight & inRange)
            {
                PosSave.OnEnemyDouble();
                base.BattleScene();
            } 
=======
                ChasePlayer();
                if (inSight & inRange)
                    StartCoroutine(AmbushPlayer());
>>>>>>> Stashed changes
        }

        else
            Patrol();

        


/*        Debug.DrawRay(walkPoint, transform.up*20, Color.green);
        if (Physics.Raycast(walkPoint, -transform.up, 3f, whatisGround))
        {
            Debug.DrawRay(walkPoint, -transform.up * 3, Color.red);
        }*/
        
    }

    void Patrol()
    {
<<<<<<< Updated upstream
=======
        anim.SetBool("isWalking", true);

        Debug.Log("HEy he erbtoertoerhtouirhtoierhoth");

/*        Debug.Log("WalkingPointSet: " + walkPointSet);
        Debug.Log("GoosePoint: " + walkPoint);
        Debug.Log("THICK SAUCE: " + gameObject.transform.position);*/

>>>>>>> Stashed changes
        if (!walkPointSet) 
            FindWalkRange();

        if (walkPointSet)
            walkPointSet = agent.SetDestination(walkPoint);

        Vector3 distanceCovered = transform.position - walkPoint;

       // Debug.Log("Men In Yellow: " + distanceCovered.magnitude);
        

        if (distanceCovered.magnitude < 1f)
            walkPointSet = false;
    }

    void FindWalkRange()
    {
        if (PatrolPointsFoward_Backwards == false)
            PatrolNumber(1, PatrolPoints.Length-1);
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
