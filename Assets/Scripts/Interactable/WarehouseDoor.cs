using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WarehouseDoor : MonoBehaviour
{
    [SerializeField] string warehouseScene;
    [SerializeField] string requiredItem1;
    [SerializeField] string requiredItem2;

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

        if (hasItem1 && hasItem2)
        {
            SceneManager.LoadScene(warehouseScene); //Load into the warehouse
        }

    }
}
