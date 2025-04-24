using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WarehouseDoor : MonoBehaviour
{
    [SerializeField] string warehouseScene;
    [SerializeField] string requiredItem1;
    [SerializeField] string requiredItem2;
    [SerializeField] TMP_Text PopupText;

    private Inventory playerInventory;
    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
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

        if (!hasItem1 || !hasItem2)
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
    private IEnumerator TextUpdate()
    {
        yield return new WaitForSeconds(2.0f);
        PopupText.text = "";
    }
}
