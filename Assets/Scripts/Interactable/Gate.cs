using UnityEngine;

public class Gate : MonoBehaviour
{
    Inventory i;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {
        foreach(Item k in i.items) //Loop through our inventory looking for the key to open the gate
        {
            if (k.itemName == "Key1")
            {
                gameObject.transform.position += new Vector3(0f, 10f, 0f); //Move the gate up if the player has the key1
            }
        }

    }
}
