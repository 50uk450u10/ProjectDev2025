using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Canvas interactCanvas; //The canvas where "Press E" text appears
    private PlayerController player = null;
    private Outline outline;
    public UnityEvent onPerformInteraction; //Events to perform on Trigger Enter
    public UnityEvent onEndInteraction; //Events to perform on Trigger Exit
    public bool isHidingSpot;
    
    void Start()
    {
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();
        outline = GetComponent<Outline>();
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

        if (outline != null)
        {
            outline.enabled = true;
        }

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

        if (outline != null)
        {
            outline.enabled = false;
        }

        if (player != null)
        {
            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }


    private void OnDestroy()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }

        if (player != null)
        {
            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }

}