using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WarehouseDoor : MonoBehaviour
{
    [SerializeField] string warehouseScene;
    [SerializeField] string requiredItem;

    private Inventory playerInventory;
    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    public void EnterBuilding()
    {
        foreach (Item k in playerInventory.items) //Loop through our inventory looking for the item to open the warehouse door
        {
            if (k.itemName == requiredItem)
            {
                SceneManager.LoadScene(warehouseScene); //Load into the warehouse
            }
        }
    }
}
