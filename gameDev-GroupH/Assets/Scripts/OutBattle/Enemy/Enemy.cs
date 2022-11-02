using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyCollider
{
    private bool dead = false;
    public NavMeshAgent agent;

    public Transform Player;

    public LayerMask whatisGround, whatisPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkpointRange;

    public float attackRange, sightRange;
    public bool inSight, inAttack;
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
            PosSave.ConfirmDeath();
        }
    }

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //inSight = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
       // inAttack = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        

        Patrol();

        Debug.DrawRay(transform.position, -transform.up*10, Color.green);
        Debug.DrawRay(walkPoint, -transform.up * 3, Color.red);
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
            Debug.Log("TRUE");
            walkPointSet = true;
        }
            
    }
}