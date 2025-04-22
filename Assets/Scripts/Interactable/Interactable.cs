using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private PlayerController player = null;
    //[SerializeField] Material baseMaterial;
    //private Material instancedMaterial;
    public UnityEvent onPerformInteraction;
    public UnityEvent onEndInteraction;
    public bool isHidingSpot;
    Canvas interactCanvas;
    

    void Start()
    {
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();
        //instancedMaterial = new Material(baseMaterial);
        //GetComponent<Renderer>().material = instancedMaterial;
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
        player = other.GetComponent<PlayerController>();
        interactCanvas.enabled = true;
        //ToggleHighlight(instancedMaterial, true);

        if (player != null)
        {
            player.on_InteractPressed += PerformInteraction;
            player.on_InteractReleased += EndInteraction;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>();
        interactCanvas.enabled = false;
        //ToggleHighlight(instancedMaterial, false);

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

    /*public void ToggleHighlight(Material instancedMaterial, bool show)
    {
        if (show)
        {
            instancedMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            instancedMaterial.DisableKeyword("_EMISSION");
        }
    }*/
}