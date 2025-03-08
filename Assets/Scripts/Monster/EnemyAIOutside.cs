using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class EnemyAIOutside : MonoBehaviour
{
    #region Properties
    private enum State { PASSIVE, APPEARING, STALKING, ATTACKING } //The 4 phases of the monster, starting from spawning in, ending with obtaining the last key
    private State currentState;
    private float appearTimer = 0f;
    private NavMeshAgent agent;

    public UnityEvent onContactPlayer;

    [SerializeField] Transform caveLocation;
    [SerializeField] Transform appearLocation;
    [SerializeField] float appearDuration = 30f;
    [SerializeField] Transform disappearLocation;
    [SerializeField] float disappearDistance = 100f;
    [SerializeField] float stalkDistance = 50f;
    [SerializeField] Transform player; //Hook in our player for calculations

    #endregion

    #region Unity Functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.APPEARING;
        IncrementState();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.PASSIVE: //In this phase, the monster does nothing, just sits in cave
                //Do nothing
                break;
            case State.APPEARING: //Once triggered, the monster will start appearing behind the player and disappear if you become too close
                if (Vector3.Distance(player.position, transform.position) <= disappearDistance) //Check to make sure the player isn't too close to the monster
                {
                    gameObject.transform.position = disappearLocation.position; //Move the monster far away and out of sight
                    appearTimer = 0f;
                }
                if (appearTimer >= appearDuration) //If we've waited long enough, move the monster
                {
                    gameObject.transform.position = appearLocation.position; //Make the monster appear at this location
                    appearTimer = 0f; //Reset timer
                }
                appearTimer += Time.deltaTime;
                break;
            case State.STALKING: //Once triggered, the monster will navmesh a certain distance towards player if they aren't looking, and run away if seen
                if (Vector3.Distance(player.position, transform.position) >= stalkDistance) //Check to make sure we aren't too close to the player
                {
                    //Obtain the dot product of the player
                    Vector3 directionToMonster = (transform.position - player.position).normalized;
                    Vector3 playerForwardDirection = player.transform.forward;

                    float dot = Vector3.Dot(playerForwardDirection, directionToMonster);

                    if (dot > 0.5f) //if the monster is in line of sight of the player
                    {
                        //sample a random position behind the monster, run to that position
                        agent.SetDestination(caveLocation.position);
                    }
                    else if (dot < 0.5f) //If monster is not in line of sight of player
                    {
                        agent.SetDestination(player.position);
                        
                    }
                    else if (Vector3.Distance(player.position, transform.position) < stalkDistance)
                    {
                        agent.ResetPath();
                        gameObject.transform.position = appearLocation.position;
                    }
                }
                break;
            case State.ATTACKING: //Once triggered, the monster will slowly pursue player 

                break;
            default:
                //Do nothing
                break;

        }
    }

    #endregion

    #region Custom Functions

    public void PerformContactInteraction() //Plug in a function here, perform it when called by collider trigger
    {
        onContactPlayer?.Invoke();
    }

    private void OnTriggerEnter(Collider other) //Check for player inside trigger colliders, perform contact function if it is player
    {
        if (other.gameObject == player.gameObject)
        {
            onContactPlayer?.Invoke();
            //PerformContactInteraction();
        }
    }

    public void IncrementState()
    {
        Debug.Log(currentState);
        currentState++; //We allow other actions in the game to call this function to change the monster's behavior
        Debug.Log(currentState);
    }

    #endregion
}
