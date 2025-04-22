using System.Collections;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] Item itemInside;
    [SerializeField] string requiredItem;
    [SerializeField] TMP_Text PopupText;

    private Inventory playerInventory;
    private bool open = false;
    private Animator anim;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    public void OpenChest()
    {
        bool reqItem = false;

        foreach (Item k in playerInventory.items) //Loop through our inventory looking for the item to open the chest
        {
            if (k.itemName == requiredItem)
            {
                if (!open) //Once we know we can open the chest, check to see if chest is already open, if not, set our values
                {
                    open = true;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    itemInside.obtainable = true;
                    reqItem = true;
                    return;
                }
                else if (open)
                {
                    //do nothing
                    return;
                }
            }

        }

        if (!reqItem)
        {
            //Output to UI that you can not interact yet
            PopupText.text = "You cannot interact with that yet";
            StartCoroutine(TextUpdate());
        }
    }

    private IEnumerator TextUpdate()
    {
        yield return new WaitForSeconds(2.0f);
        PopupText.text = "";

    }

    void SetInsideObjectStatus()
    {
        itemInside.obtainable = true;
    }
}
