using System;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAIOutside : MonoBehaviour
{

    private enum State { PASSIVE, APPEARING, STALKING, ATTACKING } //The 4 phases of the monster, starting from spawning in, ending with obtaining the last key
    private State currentState;
    private float appearTimer = 0f;

    [SerializeField] Transform appearLocation;
    [SerializeField] float appearDuration = 30f;
    [SerializeField] Transform disappearLocation;
    [SerializeField] float disappearDistance = 100f;
    [SerializeField] Transform player; //Hook in our player for calculations
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.PASSIVE;
        IncrementState();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.PASSIVE:
                //Do nothing
                break;
            case State.APPEARING:
                Debug.Log(Vector3.Distance(player.position, transform.position));
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
            case State.STALKING:

                break;
            case State.ATTACKING:

                break;
            default:
                //Do nothing
                break;

        }
    }

    public void IncrementState()
    {
        Debug.Log(currentState);
        currentState++; //We allow other actions in the game to call this function to change the monster's behavior
        Debug.Log(currentState);
    }
}
