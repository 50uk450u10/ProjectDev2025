using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    [Header("Settings")]
    public float viewDistance = 15f;
    public float stoppingDistance = 1.5f;
    public float chaseDuration = 5f;
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private float chaseTimer = 0f;
    private float lastDamageTime = 0f;

    [HideInInspector] public NavMeshAgent agent;
    public IEnemyState currentState;

    // States
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PatrolState = new EnemyPatrolState(this);
        ChaseState = new EnemyChaseState(this);

        SwitchState(PatrolState);
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(IEnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public Transform GetNextPatrolPoint()
    {
        Transform point = patrolPoints[patrolIndex];
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        return point;
    }

    public bool IsPlayerVisible()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.magnitude <= viewDistance)
        {
            if (Physics.Raycast(transform.position + Vector3.up, direction.normalized, out RaycastHit hit, viewDistance))
            {
                return hit.transform == player;
            }
        }
        return false;
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= stoppingDistance;
    }

    public void AttackPlayer()
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            Debug.Log("Attacking player!");
            player.GetComponent<PlayerHealth>()?.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }

    public void ResetChaseTimer() => chaseTimer = 0f;
    public void IncrementChaseTimer() => chaseTimer += Time.deltaTime;
    public bool ChaseTimeExceeded() => chaseTimer > chaseDuration;
}
