using Unity.VisualScripting;
using UnityEngine;

public class HideScript : MonoBehaviour
{
    PlayerController player = null;
    Interactable obj;
    EnemyInsideAI enemy;
    public GameObject playerhand;
    //Flashlight flashlight;
    Vector3 playerPos;
    float playerSpeed;
    float enemyDetect;
    float hideValue = 5f;
    Outline outline;
    MouseMovement mouseMovement;

    [SerializeField] Transform rotationObject;


    void Start()
    {
        outline = GetComponent<Outline>();
        mouseMovement = FindAnyObjectByType<MouseMovement>();
        obj = GetComponent<Interactable>();
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
                    enemyDetect = enemy.viewDistance;
                    enemy.viewDistance = hideValue;
                    //enemy.SetState(EnemyInsideAI.State.PATROLLING);
                }

                //flashlight = FindAnyObjectByType<Flashlight>();
                //if (flashlight != null) { Debug.Log("flash"); flashlight.FlashlightOn = false; }
                if(playerhand != null) { playerhand.SetActive(false); }

                outline.enabled = false;
                mouseMovement.ToggleLocked();
                playerPos = player.transform.position;
                player.transform.position = rotationObject.position;
                player.transform.rotation = rotationObject.rotation;
                playerSpeed = player.playerSpeed;
                player.playerSpeed = 0f;
                player.isHiding = true;
            }

            else if (player != null && player.isHiding == true)
            {
                if (enemy != null) { enemy.viewDistance = enemyDetect; }

                if (playerhand != null) { playerhand.SetActive (true); }
                //if (flashlight != null) { flashlight.FlashlightOn = true; }

                outline.enabled = true;
                mouseMovement.ToggleLocked();
                player.transform.position = playerPos;
                player.playerSpeed = playerSpeed;
                player.isHiding = false;
            }
        }
    }
}
