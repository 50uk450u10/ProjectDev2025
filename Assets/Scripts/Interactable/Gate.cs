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
        i = FindFirstObjectByType<Inventory>();
        finalPos = new Vector3(gameObject.transform.position.x, 5f, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (openGate && gameObject.transform.position != finalPos)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPos, 0.01f); //Move the gate up if the player has the key1
        }
    }

    public void OpenGate()
    {
        foreach(Item k in i.items) //Loop through our inventory looking for the key to open the gate
        {
            if (k.itemName == requiredItem)
            {
                openGate = true; //Open the gate if required item is found
            }
        }

    }
}
