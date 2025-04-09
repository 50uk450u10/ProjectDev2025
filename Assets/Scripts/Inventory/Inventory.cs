using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items; //Create a list to hold items that get picked up
    public int itemsInInventory = 0; //An int to easily track how many items we have
    [SerializeField] GameObject PanelParent; //Get reference to the UI Panel that holds our items
    [SerializeField] GameObject PickupPrefab; //Get a reference to a default pick up prefab for our UI


    public void AddItem(Item item) //A public function to be called when adding an item to our inventory
    {
        items.Add(item);
        itemsInInventory++;
        GameObject itemAdded;
        itemAdded = Instantiate(PickupPrefab, PanelParent.transform);
        Image myImage = itemAdded.GetComponent<Image>();
        myImage.sprite = item.sprite;
        item.gameObject.SetActive(false);

    }

    public void RemoveItem(Item item) //public function to be called when removing an item from inventory (currently unused)
    {
        items.Remove(item);
        itemsInInventory--;
    }
}
