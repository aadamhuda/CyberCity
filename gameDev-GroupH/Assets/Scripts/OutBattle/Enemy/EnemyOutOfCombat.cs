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
    public float walkpointRange;

    [SerializeField]
    public bool inSight;
    protected void Start()
    {
        SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 5;
        sc.isTrigger = true;
        PosSave.AddToDict(gameObject.name);
        dead = PosSave.getDeath(gameObject.name);
        if (dead == true)
        {
            gameObject.SetActive(false);
        }
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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        radius = 15;
        angle = 50;
        walkpointRange = 10f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (inSight)
        {
            ChasePlayer();
            if (inSight & inRange)
            {
                PosSave.OnEnemyDouble();
                base.BattleScene();
            } 
        }
        else
            Patrol();

        


        Debug.DrawRay(transform.position, -transform.up*10, Color.green);
        if (Physics.Raycast(walkPoint, -transform.up, 3f, whatisGround))
        {
            Debug.DrawRay(walkPoint, -transform.up * 3, Color.red);
        }
        
    }

    void Patrol()
    {
        if (!walkPointSet) 
            FindWalkRange();

        if (walkPointSet)
            walkPointSet = agent.SetDestination(walkPoint);

        Vector3 distanceCovered = transform.position - walkPoint;
        

        if (distanceCovered.magnitude < 3f)
            walkPointSet = false;
    }

    void FindWalkRange()
    {
        float rZ = Random.Range(-walkpointRange, walkpointRange);
        float rX = Random.Range(-walkpointRange, walkpointRange);

        walkPoint = new Vector3(transform.position.x + rX, transform.position.y, transform.position.z + rZ);



        if (Physics.Raycast(walkPoint, -transform.up, 3f, whatisGround))
        {
            walkPointSet = true;
        }
            
    }
}
