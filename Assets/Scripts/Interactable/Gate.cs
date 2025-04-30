using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Gate : MonoBehaviour
{
    [SerializeField] TMP_Text PopupText;
    [SerializeField] TMP_Text interactionText;
    private Canvas interactCanvas;


    private void Start()
    {
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactCanvas != null)
        {
            interactCanvas.enabled = true;
        }
        interactionText.text = "";
        PopupText.text = "No going back that way...";
    }

    private void OnTriggerExit(Collider other)
    {
        PopupText.text = "";
        interactionText.text = "Press E";
        if (interactCanvas != null)
        {
            interactCanvas.enabled = false;
        }
    }

}
