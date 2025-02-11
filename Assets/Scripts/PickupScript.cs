using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public void OnInteraction()
    {
        Debug.Log("Key/Meteor Obtained");
        Destroy(gameObject);
    }
}
