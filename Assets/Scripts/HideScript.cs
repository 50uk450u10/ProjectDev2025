using Unity.VisualScripting;
using UnityEngine;

public class HideScript : MonoBehaviour
{
    PlayerController player = null;
    Interactable obj;
    EnemyInsideAI enemy;
    public GameObject playerhand;
    //Flashlight flashlight;
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
                enemy = FindAnyObjectByType<EnemyInsideAI>();
                if(enemy != null)
                {
                    enemyDetect = enemy.detectionRange;
                    enemy.detectionRange = hideValue;
                    enemy.SetState(EnemyInsideAI.State.PATROLLING);
                }

                //flashlight = FindAnyObjectByType<Flashlight>();
                //if (flashlight != null) { Debug.Log("flash"); flashlight.FlashlightOn = false; }
                if(playerhand != null) { playerhand.SetActive(false); }

                playerPos = player.transform.position;
                player.transform.position = spotPos;
                playerSpeed = player.playerSpeed;
                player.playerSpeed = hideValue;
                player.isHiding = true;
            }

            else if (player != null && player.isHiding == true)
            {
                if (enemy != null) { enemy.detectionRange = enemyDetect; }

                if (playerhand != null) { playerhand.SetActive (true); }
                //if (flashlight != null) { flashlight.FlashlightOn = true; }

                player.transform.position = playerPos;
                player.playerSpeed = playerSpeed;
                player.isHiding = false;
            }
        }
    }
}
