using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private PlayerController player = null;
    public UnityEvent onPerformInteraction;
    public UnityEvent onEndInteraction;

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
        player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.on_InteractPressed += PerformInteraction;
            player.on_InteractReleased += EndInteraction;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>();

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