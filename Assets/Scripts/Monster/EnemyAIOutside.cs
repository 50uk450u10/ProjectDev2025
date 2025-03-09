using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class EnemyAIOutside : MonoBehaviour
{
    #region Properties
    private enum State { PASSIVE, APPEARING, STALKING, ATTACKING } //The 4 phases of the monster, starting from spawning in, ending with obtaining the last key
    private State currentState;
    private float appearTimer = 0f; //Timer that tracks when monster appears in phase 2
    private NavMeshAgent agent;

    public UnityEvent onContactPlayer; //A unity event to call when the monster is in contact with the player

    [SerializeField] Transform caveLocation; //This is where the monster returns to if you are looking at them in phase 3
    [SerializeField] Transform appearLocation; //Where monster appears in phase 2
    [SerializeField] float appearDuration = 30f;
    [SerializeField] Transform disappearLocation; //Where monster goes if you get too close in phase 2
    [SerializeField] float disappearDistance = 100f; //How close you can get before monster disappears
    [SerializeField] float stalkDistance = 50f; //How close monster gets in phase 3
    [SerializeField] Transform player; //Hook in our player for calculations

    #endregion

    #region Unity Functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.APPEARING; //This just for testing
        IncrementState(); //for testing
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.PASSIVE: //In this phase, the monster does nothing, just sits in cave (Phase 1)
                //Do nothing
                break;
            case State.APPEARING: //Once triggered, the monster will start appearing behind the player and disappear if you become too close (Phase 2)
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
            case State.STALKING: //Once triggered, the monster will navmesh a certain distance towards player if they aren't looking, and run away if seen (Phase 3)
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
                    if (Vector3.Distance(player.position, transform.position) < stalkDistance) //If we are too close to the monster, have them disappear behind the player
                    {
                        agent.ResetPath();
                        gameObject.transform.position = appearLocation.position;
                    }
                }
                break;
            case State.ATTACKING: //Once triggered, the monster will slowly pursue player (Phase 4)

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
            onContactPlayer?.Invoke();
        }
    }

    public void IncrementState() //function to trigger our monster's state updates (since game actions will be the triggers)
    {
        Debug.Log(currentState);
        currentState++; //We allow other actions in the game to call this function to change the monster's behavior
        Debug.Log(currentState);
    }

    #endregion
}
