using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WarehouseDoor : MonoBehaviour
{
    [SerializeField] string warehouseScene; //Scene to change to when you interact with the door
    [SerializeField] string requiredItem1;
    [SerializeField] string requiredItem2;
    [SerializeField] TMP_Text PopupText; //This is needed to connect UI to interaction prompt

    private Inventory playerInventory;
    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>(); //There should only be one inventory in the scene
    }

    public void EnterBuilding()
    {
        bool hasItem1 = false;
        bool hasItem2 = false;

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
        }

        if (!hasItem1 || !hasItem2) //If you don't have either required item, prompt player
        {
            //Output to UI that you can not interact yet
            PopupText.text = "Locked. There is a top and bottom lock...";
            StartCoroutine(TextUpdate());
        }

        if (hasItem1 && hasItem2)
        {
            SceneManager.LoadScene(warehouseScene); //Load into the warehouse
        }

    }
    private IEnumerator TextUpdate() //This allows clearing of the popup text after 2 seconds
    {
        yield return new WaitForSeconds(2.0f);
        PopupText.text = "";
    }
}
