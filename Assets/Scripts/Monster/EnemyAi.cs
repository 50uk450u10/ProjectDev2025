using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Patrol, Chase }
    public AIState currentState = AIState.Patrol;

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    [Header("Detection Settings")]
    public float detectionRange = 10f; // Sound detection range
    public float fieldOfView = 120f;   // Vision FOV in degrees
    public float viewDistance = 15f;   // Vision distance
    public float stoppingDistance = 1.5f; // Distance before stopping near player

    [Header("Chase Settings")]
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

    // PATROL
    void PatrolBehavior()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    // CHASE
    void ChaseBehavior()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            chaseTimer += Time.deltaTime;

            if (chaseTimer >= chaseDuration)
            {
                StopChasing();
            }

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

    // VISION-BASED DETECTION
    void DetectPlayer()
    {
        if (isChasing) return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);

        // Vision cone check
        if (distanceToPlayer < viewDistance && angleToPlayer < fieldOfView / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, viewDistance))
            {
                if (hit.transform == player)
                {
                    StartChasing();
                }
            }
        }
    }

    // SOUND-BASED DETECTION
    public void HearNoise(Vector3 noisePosition, float noiseRange)
    {
        if (isChasing) return;

        float distance = Vector3.Distance(transform.position, noisePosition);
        if (distance <= noiseRange)
        {
            StartChasing();
        }
    }

    void StartChasing()
    {
        currentState = AIState.Chase;
        chaseTimer = 0f;
        isChasing = true;
    }

    void StopChasing()
    {
        currentState = AIState.Patrol;
        chaseTimer = 0f;
        isChasing = false;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
    void OnDrawGizmosSelected()//FOV Tester
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 fovLine1 = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * viewDistance;
        Vector3 fovLine2 = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * viewDistance;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }




}


