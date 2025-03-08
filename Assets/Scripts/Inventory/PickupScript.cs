using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    public void OnInteraction()
    {
        
        Item item = GetComponent<Item>(); //Grabs the item component on the gameObject

        //Debug.Log("Key/Meteor Obtained");
        playerInventory.AddItem(item); //Adds the item object we created to our inventory
        Debug.Log(item.itemName + " found!");
        Destroy(gameObject); //Destroy what we picked up in the game world
    }
}
