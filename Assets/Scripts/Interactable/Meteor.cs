/*using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{
    [SerializeField] string requiredItem1;
    [SerializeField] string requiredItem2;
    [SerializeField] string requiredItem3;
    [SerializeField] public TMP_Text PopupText;
    [SerializeField] public AudioClip WinClip;
    [SerializeField] public string winLevel;
    PlayerController player;

    private Inventory playerInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    public void TurnInFragments()
    {
        bool hasItem1 = false;
        bool hasItem2 = false;
        bool hasItem3 = false;

        foreach (Item k in playerInventory.items) //Loop through our inventory looking for the item to open the warehouse door
        {
            if (k.itemName == requiredItem1)
            {
                hasItem1 = true;
            }

            if (k.itemName == requiredItem2)
            {
                hasItem2 = true;
            }

            if (k.itemName == requiredItem3)
            {
                hasItem3 = true;
            }
        }
        
        if (player.meteorCount < 3 /*!hasItem1 || !hasItem2) //If you don't have either required item, prompt player
        {
            //Output to UI that you can not interact yet
            PopupText.text = "You must gather all of the fragments for your reward...";
            StartCoroutine(TextUpdate());
        }

        if (player.meteorCount >= 3 /*hasItem1 && hasItem2 && hasItem3)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(WinClip);
            StartCoroutine(PauseForSound());
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
        SceneManager.LoadScene(winLevel); //Load into the victory scene
    }
}
*/