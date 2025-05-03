using System.Collections;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] Item itemInside;
    [SerializeField] string requiredItem;
    [SerializeField] TMP_Text PopupText;

    private Inventory playerInventory;
    private Animator anim;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        bool hasReqItem = false;
        Item reqItem = null;

        foreach (Item k in playerInventory.items) //Loop through our inventory looking for the item to open the chest
        {
            if (k.itemName == requiredItem)
            {
                anim.SetBool("Open", true);
                itemInside.obtainable = true;
                itemInside.gameObject.GetComponent<SphereCollider>().enabled = true;
                hasReqItem = true;
                gameObject.GetComponent<Interactable>().enabled = false;
                gameObject.GetComponent<Outline>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                reqItem = k;
                break;
            }
        }

        if (hasReqItem)
        {
            playerInventory.RemoveItem(reqItem);
        }

        if (!hasReqItem)
        {
            //Output to UI that you can not interact yet
            PopupText.text = "The lock looks flimsy...";
            StartCoroutine(TextUpdate());
        }
    }

    private IEnumerator TextUpdate() //Allows clearing the popup prompt after 2 seconds
    {
        yield return new WaitForSeconds(2.0f);
        PopupText.text = "";

    }

}
