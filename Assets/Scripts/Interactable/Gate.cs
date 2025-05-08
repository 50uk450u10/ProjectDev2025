using TMPro;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField] TMP_Text PopupText;
    [SerializeField] TMP_Text interactionText;
    private Canvas interactCanvas;
    private PlayerController player = null;


    private void Start()
    {
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>(); //Grab player component from collider's gameobject

        if (player != null)
        {

            if (interactCanvas != null)
            {
                interactCanvas.enabled = true;
            }
            interactionText.text = "";
            PopupText.text = "No going back that way...";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>(); //Grab player component from collider's gameobject

        if (player != null)
        {
            PopupText.text = "";
            interactionText.text = "Press E";
            if (interactCanvas != null)
            {
                interactCanvas.enabled = false;
            }
        }

    }

}
