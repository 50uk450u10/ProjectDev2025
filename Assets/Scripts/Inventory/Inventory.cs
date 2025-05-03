using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items; //Create a list to hold items that get picked up
    public List<GameObject> icons;
    public int itemsInInventory = 0; //An int to easily track how many items we have
    [SerializeField] GameObject PanelParent; //Get reference to the UI Panel that holds our items
    [SerializeField] GameObject PickupPrefab; //Get a reference to a default pick up prefab for our UI


    public void AddItem(Item item) //A public function to be called when adding an item to our inventory
    {
        items.Add(item);
        itemsInInventory++;
        GameObject itemAdded;
        itemAdded = Instantiate(PickupPrefab, PanelParent.transform);
        itemAdded.name = item.name;
        Image myImage = itemAdded.GetComponent<Image>();
        myImage.sprite = item.sprite;
        icons.Add(itemAdded);
        item.gameObject.SetActive(false);
    }

    public void RemoveItem(Item item) //public function to be called when removing an item from inventory
    {

        GameObject g = GameObject.Find(item.name);
        Destroy(g);
        items.Remove(item);
        itemsInInventory--;
    }
}
