using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class Interactable : MonoBehaviour
{
    private Canvas interactCanvas; //The canvas where "Press E" text appears
    private PlayerController player = null;
    private Outline outline;
    public UnityEvent onPerformInteraction; //Events to perform on Trigger Enter
    public UnityEvent onEndInteraction; //Events to perform on Trigger Exit
    public bool isHidingSpot;
    public bool isMeteor;
    //Meteor meteor;
    [SerializeField] public TMP_Text PopupText;
    [SerializeField] public AudioClip WinClip;



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

    public void EndGame()
    {
        if (player.meteorCount >= 3 && isMeteor)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(WinClip);
            StartCoroutine(PauseForSound());
        }
        else if (player.meteorCount < 3 && isMeteor)
        {
            PopupText.text = "You must gather all of the fragments for your reward...";
            StartCoroutine(TextUpdate());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>(); //Grab player component from collider's gameobject

        if (player != null)
        {
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
        if (interactCanvas != null)
        {
            interactCanvas.enabled = false; //Turn off our text prompt
        }

        if (player != null)
        {
            
            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }

    private void OnDisable()
    {
        if (interactCanvas != null)
        {
            interactCanvas.enabled = false; //Turn off our text prompt
        }

        if (player != null)
        {

            player.on_InteractPressed -= PerformInteraction;
            player.on_InteractReleased -= EndInteraction;
        }
    }

    private IEnumerator TextUpdate() //This allows clearing of the popup text after 2 seconds
    {
        yield return new WaitForSeconds(2.0f);
        PopupText.text = "";
    }

    private IEnumerator PauseForSound()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(sceneName: "VictoryScene"); //Load into the victory scene
    }
}