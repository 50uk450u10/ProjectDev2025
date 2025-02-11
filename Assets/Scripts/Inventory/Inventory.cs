using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public int itemsInInventory = 0;
    [SerializeField] GameObject PanelParent;
    [SerializeField] GameObject PickupPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        itemsInInventory++;
        GameObject itemAdded;
        itemAdded = Instantiate(PickupPrefab, PanelParent.transform);
        Image myImage = itemAdded.GetComponent<Image>();
        myImage.sprite = item.sprite;
        item.gameObject.SetActive(false);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        itemsInInventory--;
    }
}
