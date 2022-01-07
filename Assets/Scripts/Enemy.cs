using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
       
    public LayerMask PlayerMask;
    public LayerMask GroundMask;    
    public bool playerAlive = true;
    public bool CanMove = true;
    private Player playerScript;
    private Transform player;
    private NavMeshAgent agent;
    //Patroling
    public Vector3 walkPoint;
    public float WalkPointRange;
    private bool walkPointSet;

    //Attacking
    public int timeBetweenAttacks = 2;
    private bool alreadyAttacked;

    //State
    public int sightRange,attackRange;
    private bool playerInSightRange; 
    private bool playerInAttackRange;

    public Material mainMaterial;
    public Material visionMaterial;


    private Animator animator;
    private int isRunningHash;
    private int isAttackingHash;
    private int dancingHash;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        playerScript = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking");
        dancingHash = Animator.StringToHash("isDancing");
    }
    private void Update()
    {
        if (playerAlive && CanMove)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

            if (!playerInSightRange && !playerInAttackRange)
                Patroling();

            if (playerInSightRange && !playerInAttackRange)
                ChasePlayer();

            if (playerInSightRange && playerInAttackRange)
                AttackPlayer();

            if (Mathf.Abs(agent.velocity.magnitude) > .05)
            {
                animator.SetBool(isRunningHash, true);
            }
            else
                animator.SetBool(isRunningHash, false);
        }
        else if(!playerAlive)
            animator.SetBool(dancingHash, true);

    }
    private void Patroling()
    {
        if (!walkPointSet)
            SearchForWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void ChasePlayer()
    { 
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetBool(isAttackingHash, true);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
            animator.SetBool(isAttackingHash, false);

    }
    public void PlayerWon()
    {
        CanMove = false;
        agent.isStopped = true;
        animator.SetBool(isRunningHash, false);
    }
    public void DealDamage()
    {
        if(Vector3.Distance(transform.position,player.transform.position) <= 2.5f)
        playerScript.TakeDamage();
    }

    private void SearchForWalkPoint()
    {
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundMask))
            walkPointSet = true;
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }
}
