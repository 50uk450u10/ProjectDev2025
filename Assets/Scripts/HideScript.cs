/*using Unity.VisualScripting;
using UnityEngine;

public class HideScript : MonoBehaviour
{
    PlayerController player = null;
    Interactable obj;
    EnemyAI enemy;
    Flashlight flashlight;
    Vector3 spotPos;
    Vector3 playerPos;
    float playerSpeed;
    float enemyDetect;
    float hideValue = 0f;

    void Start()
    {
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
                enemy = FindAnyObjectByType<EnemyAI>();
                if(enemy != null)
                {
                    enemyDetect = enemy.detectionRange;
                    enemy.detectionRange = hideValue;
                }

                flashlight = FindAnyObjectByType<Flashlight>();
                if (flashlight != null) { Debug.Log("flash"); flashlight.FlashlightOn = false; }

                playerPos = player.transform.position;
                player.transform.position = spotPos;
                playerSpeed = player.speed;
                player.speed = hideValue;
                player.isHiding = true;
            }

            else if (player != null && player.isHiding == true)
            {
                if (enemy != null) { enemy.detectionRange = enemyDetect; }

                if (flashlight != null) { flashlight.FlashlightOn = true; }

                player.transform.position = playerPos;
                player.speed = playerSpeed;
                player.isHiding = false;
            }
        }
    }
}
*/