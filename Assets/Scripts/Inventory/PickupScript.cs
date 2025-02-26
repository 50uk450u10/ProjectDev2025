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
        Item item = GetComponent<Item>();

        //Debug.Log("Key/Meteor Obtained");
        playerInventory.AddItem(gameObject.GetComponent<Item>());
        Debug.Log(item.itemName + " found!");
        Destroy(gameObject);
    }
}
