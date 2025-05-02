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
    private float playerLevel = 1f;
    private float monsterLevel = 1f;

    public UnityEvent onContactPlayer; //A unity event to call when the monster is in contact with the player
    public float detectionRange;
    public float viewDistance;

    [SerializeField] float deAggroTime = 5f;
    [SerializeField] Transform[] midPatrolLocations;
    [SerializeField] Transform[] topPatrolLocations;
    [SerializeField] Transform[] bottomPatrolLocations;
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

        anim.SetFloat("speed", agent.velocity.magnitude); //Animation is tied to speed of nav agent
        

        switch (currentState)
        {
            case State.PASSIVE: //In this phase, the monster does nothing, chills for 3 seconds then transitions to patrolling

                if (agent.speed != 2f)
                    agent.speed = 2f;

                if (chillTimer < chillTime)
                {
                    chillTimer += Time.deltaTime;
                }
                else if (chillTimer >= chillTime)
                {
                    chillTimer = 0f;
                    currentPatrolLocation = PickRandomPatrolPoint();
                    agent.SetDestination(currentPatrolLocation.position);
                    currentState = State.PATROLLING;
                }
                break;
            case State.PATROLLING: //Once triggered, the monster will start patrolling around the map
                if (IsPlayerVisible())
                {
                    source.PlayOneShot(roarClip);
                    agent.speed = 4f;
                    currentState = State.PURSUING;
                }

                if (ArrivedAtPatrolPoint())
                {
                    currentPatrolLocation = PickRandomPatrolPoint();
                    if (monsterLevel != playerLevel)
                    {
                        agent.enabled = false;
                        monsterLevel = playerLevel;
                        transform.position = PickFarthestLocation().position;
                        agent.enabled = true;
                        currentPatrolLocation = PickRandomPatrolPoint();
                    }
                    agent.SetDestination(currentPatrolLocation.position);
                }
                break;
            case State.PURSUING: //Once triggered, the monster will chase the player unless they are out of sight for 5 seconds or enter a hiding spot
                if (deAggroTimer >= deAggroTime)
                {
                    deAggroTimer = 0f;
                    currentState = State.PASSIVE;
                }
                else if (!IsPlayerVisible())
                {
                    deAggroTimer += Time.deltaTime;
                }
                else if (IsPlayerVisible())
                {
                    deAggroTimer = 0f;
                }
                Debug.Log(deAggroTimer);
                agent.SetDestination(player.position);
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
            CheckPlayerLevel();//Compare monster's location to player to determine if they are on a different level
            return true;
        }
        else return false;
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
                    return true;
                }
            }

        }

        return false;
    }

    public void CheckPlayerLevel()
    {
        if (player.position.y - transform.position.y > 4)
        {
            playerLevel += 1;
            
        }
        else if (player.position.y - transform.position.y < -4)
        {
            playerLevel -= 1;
            
        }
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    private Transform PickRandomPatrolPoint()
    {


        switch (playerLevel)
        {
            case 0: //player is on the bottom level
                var random = new System.Random();
                var loc = random.Next(0, bottomPatrolLocations.Length);
                return bottomPatrolLocations[loc];

            case 1: //player is on the middle level
                var random1 = new System.Random();
                var loc1 = random1.Next(0, midPatrolLocations.Length);
                return midPatrolLocations[loc1];

            case 2: //player is on the top level
                var random2 = new System.Random();
                var loc2 = random2.Next(0, topPatrolLocations.Length);
                return topPatrolLocations[loc2];
                
            default: //Something's wrong if you hit this lol
                Debug.Log("Oops");
                return null;
        }


    }

    private Transform PickFarthestLocation()
    {
        switch (playerLevel)
        {
            case 0: //player is on the bottom level
                return FindFarthestSpawnPoint(bottomPatrolLocations);

            case 1: //player is on the middle level
                return FindFarthestSpawnPoint(midPatrolLocations);

            case 2: //player is on the top level
                return FindFarthestSpawnPoint(topPatrolLocations);

            default: //Something's wrong if you hit this lol
                Debug.Log("Oops");
                return null;
        }
    }

    private Transform FindFarthestSpawnPoint(Transform[] t)
    {
        Transform farthestPoint = null;
        t[0] = farthestPoint;

        for (int i = 0; i < t.Length; i++)
        {
            if (Vector3.Distance(t[i].position, player.position) > Vector3.Distance(farthestPoint.position, player.position))
            {
                farthestPoint = t[i];
            }
        }

        return farthestPoint;
    }
    #endregion
}
