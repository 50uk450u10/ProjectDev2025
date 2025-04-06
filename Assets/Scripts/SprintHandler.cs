using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SprintHandler : MonoBehaviour
{
    [SerializeField] float sprintSpeed = 10; // allows editor to set sprint speed
    [SerializeField] float sprintCost = 33; // sets how much of the total stamina is taken per second
    [SerializeField] float sprintCooldown = 2.5f; // sets how long (in seconds) the sprint takes before recharging
    [SerializeField] float sprintRechargeRate = 20; // sets how much stamina is replenished a second
    [SerializeField] float maxStamina = 100; // sets the maximum stamina the players has
    [SerializeField] float currentStamina; //shows current stamina the player has
    [SerializeField] bool isSprinting;
    PlayerController playerController;
    [SerializeField] Image sprintBar;
    private Coroutine recharge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        isSprinting = false;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentStamina > 0)
            {
                isSprinting = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            playerController.playerSpeed = playerController.speed;
        }

        if (isSprinting == true)
        {
            playerController.playerSpeed = sprintSpeed;
            currentStamina -= sprintCost * Time.deltaTime;
            if (currentStamina < 0 || currentStamina == 0)
            {
                currentStamina = 0;
                isSprinting = false;
                playerController.playerSpeed = playerController.speed;
            }
            sprintBar.fillAmount = currentStamina / maxStamina;
            if (recharge != null)
            {
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(sprintCooldown);

        while (currentStamina < maxStamina)
        {
            currentStamina += sprintRechargeRate / 20f;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            sprintBar.fillAmount = currentStamina / maxStamina;
            yield return new WaitForSeconds(.05f);
        }
    }
}
