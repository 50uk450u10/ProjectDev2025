using UnityEngine;

public class PickupScript : MonoBehaviour
{
    EnemyAIOutside monster;
    Canvas interactCanvas;

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
        monster = FindAnyObjectByType<EnemyAIOutside>();
        var canvasParent = GameObject.Find("InteractionCanvas");
        interactCanvas = canvasParent.GetComponent<Canvas>();
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
            //interactCanvas.enabled = false; //turns off "press E" popup
        }
        else if (item.obtainable == false)
        {
            return;
        }
    }
}
