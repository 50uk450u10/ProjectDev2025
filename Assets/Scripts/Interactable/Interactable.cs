using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private PlayerController player = null;
    public UnityEvent onPerformInteraction; //Events to perform on Trigger Enter
    public UnityEvent onEndInteraction; //Events to perform on Trigger Exit
    public bool isHidingSpot;
    Canvas interactCanvas; //The canvas where "Press E" text appears
    

    void Start()
    {
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();

    }

    //Invoke Unity Event to perform interaction logic
    public void PerformInteraction()
    {
        onPerformInteraction?.Invoke();
    }

    public void EndInteraction()
    {
        onEndInteraction?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>(); //Grab player component from collider's gameobject
        interactCanvas.enabled = true; //Turn on our text prompt
        

        if (player != null)
        {
            player.on_InteractPressed += PerformInteraction;
            player.on_InteractReleased += EndInteraction;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>(); //Grab player component from collider's gameobject
        interactCanvas.enabled = false; //Turn off our text prompt


        if (player != null)
        {
            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }


    private void OnDestroy()
    {
        if (player != null)
        {
            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }

}