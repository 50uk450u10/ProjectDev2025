using UnityEngine;

public class MeteoriteFragment : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>();
    }

    public void onInteraction()
    {
        if (player != null)
        {
            player.meteorCount += 1;
            Debug.Log(player.meteorCount);
            GameObject.Destroy(gameObject);
        }
    }
}
