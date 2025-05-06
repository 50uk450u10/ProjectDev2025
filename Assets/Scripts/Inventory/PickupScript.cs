using UnityEngine;

public class PickupScript : MonoBehaviour
{
    EnemyAIOutside monster;
    EnemyInsideAI insideMonster;

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
        monster = FindAnyObjectByType<EnemyAIOutside>();
        insideMonster = FindAnyObjectByType<EnemyInsideAI>();
    }

    public void OnInteraction()
    {
        
        Item item = GetComponent<Item>(); //Grabs the item component on the gameObject

        if (item.obtainable)
        {
            playerInventory.AddItem(item); //Adds the item object we created to our inventory
            if (monster != null)
            {
                monster.IncrementState(); //If we are outside and pickup an item, increment the monster's state
            }
            if (insideMonster != null)
            {
                insideMonster.AngerMonster();
            }

        }
        else if (item.obtainable == false)
        {
            return;
        }
    }
}
