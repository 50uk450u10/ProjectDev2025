using System.Collections;
using UnityEngine;

public class SprintHandler : MonoBehaviour
{
    [SerializeField] float sprintSpeed = 6; // allows editor to set sprint speed
    [SerializeField] float sprintDuration = 3; // sets how long (in seconds) the sprint lasts
    private float scriptOnlySprintDuration;
    [SerializeField] float sprintCooldown = 5; // sets how long (in seconds) the sprint takes to fully recharge
    private float scriptOnlySprintCooldown;
    private bool canSprint;
    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        canSprint = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSprint == true)
        {
            canSprint = false;
            StartSprinting();
        }
    }

    private void StartSprinting()
    {
        playerController.playerSpeed = sprintSpeed;
        scriptOnlySprintDuration = sprintDuration;
        scriptOnlySprintCooldown = sprintCooldown;
        StartCoroutine(Sprinting());
    }

    IEnumerator Sprinting()
    {
        Debug.Log("Sprinting at " + playerController.playerSpeed + " speed");
        while (scriptOnlySprintDuration > 0)
        {
            scriptOnlySprintDuration -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        playerController.playerSpeed = playerController.speed;
        StartCoroutine(SprintRecharge());
        scriptOnlySprintDuration = sprintDuration;
        Debug.Log("Sprint ends, speed is back to " + playerController.playerSpeed);
    }

    IEnumerator SprintRecharge()
    {
        Debug.Log("Recharging sprint");
        while (scriptOnlySprintCooldown > 0)
        {
            scriptOnlySprintCooldown -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        canSprint = true;
        scriptOnlySprintCooldown = sprintCooldown;
        Debug.Log("Sprint ready");
    }
}
