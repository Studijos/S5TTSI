using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour
{
    public float health = 100;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    // Patrolling
    public Vector3 destination;
    bool destinationSet;
    public float destinationRange;

    // Atacking
    public float timeBetweenAttacks;
    bool attacked;
    //public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInAttackRange, playerInSightRange;

    private void Start()
    {
        health = 100;
        player = GameObject.FindGameObjectWithTag("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        // Check if player is seen and can be attacked
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange) Chase();
        //if (playerInAttackRange && playerInSightRange) Attack();
    }
    private void SearchDestination()
    {
        float randomZ = Random.Range(-destinationRange, destinationRange);
        float randomX = Random.Range(-destinationRange, destinationRange);
        destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if destination point is on ground
        if (Physics.Raycast(destination, -transform.up, 2f, groundMask))
        {
            destinationSet = true;
        }
    }

    private void Patrol()
    {
        if (!destinationSet) SearchDestination();
       
        if (destinationSet)
        {
            agent.SetDestination(destination);
        }
        Vector3 distanceToDestination = transform.position - destination;

        // if destination is reached set destinationSet to false to look for a new one
        if(distanceToDestination.magnitude < 1f)
        {
            destinationSet = false;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //if(!attacked)
        //{
        //    /// attack code
        //    Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        //    //rb.AddForce(transform.up * 2f, ForceMode.Impulse);

        //    /// 
        //    attacked = true;
        //    Invoke(nameof(ResetAttack), timeBetweenAttacks);
        //}
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("IIIIIIIIIIM HIIIIIIIIIIIIIIIIIIIT");
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
            //Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
