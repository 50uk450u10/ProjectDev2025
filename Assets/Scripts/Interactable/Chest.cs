using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Item itemInside;
    [SerializeField] string requiredItem;

    private Inventory playerInventory;
    private bool open = false;
    private Animator anim;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    public void OpenChest()
    {
        foreach (Item k in playerInventory.items) //Loop through our inventory looking for the item to open the chest
        {
            if (k.itemName == requiredItem)
            {
                if (!open) //Once we know we can open the chest, check to see if chest is already open, if not, set our values
                {
                    open = true;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (open)
                {
                    //do nothing
                    return;
                }
            }
        }
    }

    public void SetInsideObjectStatus()
    {
        itemInside.obtainable = true;
    }
}
