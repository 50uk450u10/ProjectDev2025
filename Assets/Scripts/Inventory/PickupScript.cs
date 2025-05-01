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
            Destroy(gameObject); //Destroy what we picked up in the game world
            if (monster != null)
            {
                monster.IncrementState(); //If we are outside and pickup an item, increment the monster's state
            }
            
        }
        else if (item.obtainable == false)
        {
            return;
        }
    }
}
