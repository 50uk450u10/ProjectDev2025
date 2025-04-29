using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyInsideAI : MonoBehaviour
{
    #region Properties
    public enum State { PASSIVE, PATROLLING, PURSUING, ATTACKING } //The 4 phases of the monster, starting from spawning in, ending with obtaining the last key
    private State currentState;
    private float chillTimer = 0f; //Timer that tracks how long before starts patrolling
    private float chillTime = 3f;
    private float deAggroTimer = 0f;
    private NavMeshAgent agent;
    private Animator anim;
    private Transform currentPatrolLocation;

    public UnityEvent onContactPlayer; //A unity event to call when the monster is in contact with the player
    public float detectionRange;
    public float viewDistance;

    [SerializeField] float deAggroTime = 5f;
    [SerializeField] Transform[] patrolLocations;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip roarClip;
    [SerializeField] AudioClip attackClip;
    [SerializeField] Transform player; //Hook in our player for calculations

    #endregion

    #region Unity Functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.PASSIVE; //Beginning State
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);

        anim.SetFloat("speed", agent.velocity.magnitude);

        switch (currentState)
        {
            case State.PASSIVE: //In this phase, the monster does nothing, chills for 3 seconds then transitions to patrolling
                //Do nothing
                if (chillTimer < chillTime)
                {
                    chillTimer += Time.deltaTime;
                }
                else if (chillTimer >= chillTime)
                {
                    chillTimer = 0f;
                    currentPatrolLocation = pickNewPatrolPoint();
                    agent.SetDestination(currentPatrolLocation.position);
                    currentState = State.PATROLLING;
                }
                break;
            case State.PATROLLING: //Once triggered, the monster will start patrolling around the map
                if (IsPlayerVisible())
                {
                    source.PlayOneShot(roarClip);
                    currentState = State.PURSUING;
                }

                if (ArrivedAtPatrolPoint())
                {
                    currentPatrolLocation = pickNewPatrolPoint();
                    agent.SetDestination(currentPatrolLocation.position);
                }
                break;
            case State.PURSUING: //Once triggered, the monster will chase the player unless they are out of sight for 5 seconds or enter a hiding spot
                if (deAggroTimer >= deAggroTime)
                {
                    deAggroTimer = 0f;
                    currentState = State.PASSIVE;
                }
                if (!IsPlayerVisible())
                {
                    deAggroTimer += Time.deltaTime;
                }
                if (IsPlayerVisible())
                {
                    agent.SetDestination(player.position);
                }
                break;
            case State.ATTACKING: //Once triggered, the monster will kill the player
                onContactPlayer.Invoke();
                break;
            default:
                //Do nothing
                break;

        }
    }

    #endregion

    #region Custom Functions

    private void OnTriggerEnter(Collider other) //Check for player inside trigger colliders, perform contact function if it is player
    {
        if (other.gameObject == player.gameObject)
        {
            currentState = State.ATTACKING;
        }
    }

    private bool ArrivedAtPatrolPoint()
    {
        if (gameObject.transform.position.x == currentPatrolLocation.position.x && gameObject.transform.position.z == currentPatrolLocation.position.z)
        {
            return true;
        }
        else return false;
    }

    public void IncrementState() //function to trigger our monster's state updates (since game actions will be the triggers)
    {
        /*currentState++; //We allow other actions in the game to call this function to change the monster's behavior
        if (currentState != State.ATTACKING)
        {
            source.PlayOneShot(roarClip);
        }
        if (currentState == State.PATROLLING)
        {
            gameObject.transform.position = currentHidingSpot.position;
            agent.enabled = true;
            agent.speed = 3.0f;
        }
        else if (currentState == State.ATTACKING)
        {
            source.Stop();
            source.PlayOneShot(attackClip);
            source.PlayOneShot(phase4Clip);
            agent.speed = 6.0f;
        }
        Debug.Log(currentState);*/
    }

    public bool IsPlayerVisible()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 forwardDirection = transform.forward;

        float dot = Vector3.Dot(forwardDirection, directionToPlayer);

        if (dot > 0.5f)
        {
            Debug.DrawRay(transform.position, directionToPlayer * viewDistance, Color.green);
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, viewDistance))
            {
                if (hit.transform.gameObject == player.gameObject)
                {
                    Debug.Log(gameObject.name + " sees the player!");
                    return true;
                }
            }

        }

        return false;
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    private Transform pickNewPatrolPoint()
    {
        var random = new System.Random();
        var loc = random.Next(0, patrolLocations.Length);
        return patrolLocations[loc];
    }
    #endregion
}
