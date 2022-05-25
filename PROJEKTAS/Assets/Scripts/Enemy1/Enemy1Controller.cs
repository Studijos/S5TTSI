using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour
{
    public GameObject zombie;
    public ZombieManager zombieManager;

    Animator myAnimator;
    [SerializeField]
    float walkSpeed = 3f;
    [SerializeField]
    float runSpeed = 5f;
    [SerializeField] 
    float stopDistance;

    [SerializeField]
    int damage;

    public float health = 100;
    public bool dead = false;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    // Patrolling
    //public Vector3 destination;
    //public float destinationRange;
    

    // Atacking
    float lastAttackTime = 0;
    float attackCooldown = 2;
    public float timeBetweenAttacks;

    // States
    public float sightRange, attackRange;
    public bool playerInAttackRange, playerInSightRange;

    private void Start()
    {
        zombieManager = FindObjectOfType<ZombieManager>();
        health = 100;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Check if player is seen and can be attacked
        //OnDrawGizmosSelected();
        if(!dead)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= stopDistance)
            {
                Attack();
                //Debug.Log("distance to player attacking: ");
                //Debug.Log(distanceToPlayer);

            }
            else if (distanceToPlayer > stopDistance && distanceToPlayer <= sightRange)
            {
                //Debug.Log("distance to player chasing: ");
                //Debug.Log(distanceToPlayer);
                Chase();
                //Debug.Log(transform.position);
                //Debug.Log(player.transform.position);

            }
            else
            {
                Patrol();
            }

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        }
        else
        {
            var rb = this.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
            agent.enabled = false;
        }
        //if (!playerInSightRange && !playerInAttackRange) Patrol();
        //if (playerInSightRange && !playerInAttackRange) Chase();
        //if (playerInAttackRange && playerInSightRange) Attack();
        
    }
    /*
    private void SearchDestination()
    {
        float randomZ = Random.Range(-destinationRange, destinationRange);
        float randomX = Random.Range(-destinationRange, destinationRange);
        destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //destinationSet = true;

        // Check if destination point is on ground
        
        if (Physics.Raycast(destination, -transform.up, 2f, groundMask))
        {
            destinationSet = true;
        }
        
    }
    */

    private void Patrol()
    {
        //agent.isStopped = false;
        agent.transform.LookAt(player.transform.position);
        agent.speed = walkSpeed;
        agent.SetDestination(player.transform.position);
        myAnimator.enabled = true;
        myAnimator.SetBool("isAttacking", false);
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isWalking", true);

        /*
        agent.isStopped = false;

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
        */
    }

    private void Chase()
    {
        //agent.isStopped = false;
        agent.transform.LookAt(player.transform.position);
        agent.speed = runSpeed;
        agent.SetDestination(player.transform.position);
        myAnimator.enabled = true;
        myAnimator.SetBool("isWalking", false);
        myAnimator.SetBool("isAttacking", false);
        myAnimator.SetBool("isRunning", true);
    }

    private void Attack()
    {
        //agent.transform.LookAt(player.transform.position);
        agent.SetDestination(agent.transform.position);
        

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            player.GetComponent<Player>().TakeDamage(damage);
        }
        myAnimator.SetBool("isWalking", false);
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isAttacking", true);
        //myAnimator.SetBool("isWalking", false);
        /*
        if(!attacked)
        {
            /// attack code
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        */
    }

    //private void ResetAttack()
    //{
    //    attacked = false;
    //}

    public void TakeDamage(int damage)
    {
        Debug.Log("IIIIIIIIIIM HIIIIIIIIIIIIIIIIIIIT");
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        
        myAnimator.SetBool("isDead", true);
        dead = true;
        zombieManager.enemyCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
