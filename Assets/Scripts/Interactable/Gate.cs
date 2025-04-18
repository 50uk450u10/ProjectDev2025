using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Gate : MonoBehaviour
{
    Inventory i;
    Vector3 finalPos;
    bool openGate = false;
    [SerializeField] string requiredItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = FindFirstObjectByType<Inventory>(); //Find the inventory (there should only be one) in scene
        finalPos = new Vector3(gameObject.transform.position.x, 10f, gameObject.transform.position.z); //Set a final position for the gate to transition to on open
    }

    // Update is called once per frame
    void Update()
    {
        if (openGate && gameObject.transform.position != finalPos) //If gate bool is true, and the gate isn't in its final position
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, 0.01f); //Move the gate up
        }
    }

    public void OpenGate()
    {
        foreach(Item k in i.items) //Loop through our inventory looking for the item to open the gate
        {
            if (k.itemName == requiredItem)
            {
                openGate = true; //Open the gate if required item is found
            }
        }

    }
}
