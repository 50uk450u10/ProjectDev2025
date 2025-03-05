using Unity.VisualScripting;
using UnityEngine;

public class HideScript : MonoBehaviour
{
    PlayerController player = null;
    Interactable obj;
    EnemyAI enemy;
    Vector3 spotPos;
    Vector3 playerPos;
    float playerSpeed;
    //float enemyDetect;
    float hideValue = 0f;

    void Start()
    {
        //enemy = FindAnyObjectByType<EnemyAI>();
        obj = GetComponent<Interactable>();
        spotPos = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>();
    }

    public void onInteraction()
    {
        if (obj.isHidingSpot == true)
        {
            if (player != null && player.isHiding == false)
            {
                Debug.Log("hide");
                playerPos = player.transform.position;
                player.transform.position = spotPos;
                playerSpeed = player.speed;
                player.speed = hideValue;
                //enemyDetect = enemy.detectionRange;
                //enemy.detectionRange = hideValue;
                player.isHiding = true;
            }

            else if (player != null && player.isHiding == true)
            {
                Debug.Log("unhide");
                player.transform.position = playerPos;
                player.speed = playerSpeed;
                //enemy.detectionRange = enemyDetect;
                player.isHiding = false;
            }
        }
    }
}
