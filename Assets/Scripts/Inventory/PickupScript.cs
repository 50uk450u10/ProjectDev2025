using UnityEngine;

public class PickupScript : MonoBehaviour
{
    EnemyAIOutside monster;

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
        monster = FindAnyObjectByType<EnemyAIOutside>();
    }

    public void OnInteraction()
    {
        
        Item item = GetComponent<Item>(); //Grabs the item component on the gameObject

        if (item.obtainable)
        {
            playerInventory.AddItem(item); //Adds the item object we created to our inventory
            Debug.Log(item.itemName + " found!");
            Destroy(gameObject); //Destroy what we picked up in the game world
            monster.IncrementState();
        }
        else if (item.obtainable == false)
        {
            return;
        }
    }
}
