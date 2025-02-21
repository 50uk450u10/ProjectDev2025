using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Patrol, Chase }
    public AIState currentState = AIState.Patrol;

    public Transform player;
    public Transform[] patrolPoints; // Assign patrol points in the Inspector
    private int currentPatrolIndex = 0;

    private NavMeshAgent agent;
    public float detectionRange = 10f; // Detection range for the player
    public float stoppingDistance = 1.5f; // Distance before stopping near player

    public float chaseDuration = 5f; // Adjustable in Inspector
    private float chaseTimer = 0f;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        DetectPlayer();

        switch (currentState)
        {
            case AIState.Patrol:
                PatrolBehavior();
                break;
            case AIState.Chase:
                ChaseBehavior();
                break;
        }
    }

    void PatrolBehavior()
    {
        if (patrolPoints.Length == 0) return;

        // Move to the next patrol point when the enemy reaches the current one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void ChaseBehavior()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Constantly update target position
            chaseTimer += Time.deltaTime; // Track how long enemy has been chasing

            // Stop chasing after chaseDuration (set in Inspector)
            if (chaseTimer >= chaseDuration)
            {
                StopChasing();
            }

            // Stop moving when close enough
            if (agent.remainingDistance <= stoppingDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && !isChasing)
        {
            StartChasing();
        }
    }

    void StartChasing()
    {
        currentState = AIState.Chase;
        chaseTimer = 0f; // Reset chase timer
        isChasing = true;
    }

    void StopChasing()
    {
        currentState = AIState.Patrol;
        chaseTimer = 0f; // Reset the timer
        isChasing = false; // Allow chasing again after returning to patrol

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
}
