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
    private bool phase4 = false;
    private Animator anim;
    private Transform currentHidingSpot;

    public UnityEvent onContactPlayer; //A unity event to call when the monster is in contact with the player

    [SerializeField] Transform[] hidingLocations;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip spawnClip;
    [SerializeField] AudioClip phase4Clip;
    [SerializeField] Transform caveLocation; //This is where the monster returns to if you are looking at them in phase 3
    [SerializeField] Transform appearLocation; //Where monster appears in phase 2
    [SerializeField] float appearDuration = 30f;
    [SerializeField] Transform disappearLocation; //Where monster goes if you get too close in phase 2
    [SerializeField] float disappearDistance = 100f; //How close you can get before monster disappears
    [SerializeField] float stalkDistance = 50f; //How close monster gets in phase 3
    [SerializeField] Transform player; //Hook in our player for calculations
    [SerializeField] Flashlight pFlashlight;

    #endregion

    #region Unity Functions
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.PASSIVE; //Beginning State
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        anim = GetComponentInChildren<Animator>();
        currentHidingSpot = caveLocation;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);

        switch (currentState)
        {
            case State.PASSIVE: //In this phase, the monster does nothing, just sits in cave (Phase 1)
                //Do nothing
                break;
            case State.APPEARING: //Once triggered, the monster will start appearing behind the player and disappear if you become too close (Phase 2)
                if (Vector3.Distance(player.position, transform.position) <= disappearDistance) //Check to make sure the player isn't too close to the monster
                {
                    gameObject.transform.position = disappearLocation.position; //Move the monster far away and out of sight
                    pFlashlight.ToggleFlashlight();
                    appearTimer = 0f;
                }
                if (appearTimer >= appearDuration) //If we've waited long enough, move the monster
                {
                    gameObject.transform.position = appearLocation.position; //Make the monster appear at this location
                    transform.LookAt(player);
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

                    if (dot > 0.9f) //if the monster is in line of sight of the player
                    {
                        //Run to a hiding spot
                        agent.SetDestination(currentHidingSpot.position);
                        agent.speed = 8.0f;
                    }
                    else if (dot < 0.9f) //If monster is not in line of sight of player
                    {
                        agent.SetDestination(player.position);
                        agent.speed = 3.0f;
                    }
                }
                if (Vector3.Distance(player.position, transform.position) < stalkDistance) //If we are too close to the monster, have them disappear behind the player
                {
                    agent.ResetPath();
                    gameObject.transform.position = appearLocation.position;
                    currentHidingSpot = pickNewHidingSpot();
                    pFlashlight.ToggleFlashlight();
                }
                break;
            case State.ATTACKING: //Once triggered, the monster will slowly pursue player (attempting to kill) (Phase 4)
                if (!phase4) //use bool so monster only teleports to cave once (not each frame)
                {
                    gameObject.transform.position = caveLocation.position; //First thing we do when entering phase is move monster to the cave
                    phase4 = true;
                }
                agent.SetDestination(player.position); //Pursue player, at which point contacting player will "kill" them

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
        currentState++; //We allow other actions in the game to call this function to change the monster's behavior
        if (currentState == State.STALKING)
        {
            gameObject.transform.position = currentHidingSpot.position;
            agent.enabled = true;
            agent.speed = 3.0f;
        }
        else if (currentState == State.ATTACKING)
        {
            source.Stop();
            source.PlayOneShot(phase4Clip);
            agent.speed = 6.0f;
        }
        Debug.Log(currentState);
    }

    private Transform pickNewHidingSpot()
    {
        var random = new System.Random();
        var loc = random.Next(0, hidingLocations.Length);
        return hidingLocations[loc];
    }
    #endregion
}
