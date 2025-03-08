using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] float speed; // allows editor to set default speed
    [SerializeField] float sprintSpeed; // allows ediotr to set sprint speed
    public bool isSprinting;
    private float playerSpeed; 
    IA_Player movement;
    public UnityAction on_InteractPressed;
    public UnityAction on_InteractReleased;
    [SerializeField] Inventory myInventory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //movement = new IA_Player();
        //movement.PlayerMovement.Enable();
        playerSpeed = speed;
    }

    private void OnEnable()
    {
        movement = new IA_Player();
        movement.PlayerMovement.Enable();
    }

    private void OnDisable()
    {
        movement.PlayerMovement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = movement.PlayerMovement.Movement.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            on_InteractPressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            on_InteractReleased?.Invoke();
        }
        
        if(isSprinting == true)
        {
            playerSpeed = sprintSpeed;
        }
        else
        {
            playerSpeed = speed;
        }
    }
    private void FixedUpdate()
    {
        var newInput = GetCameraBasedInput(input, Camera.main);

        var newVelocity = new Vector3(newInput.x * playerSpeed, rb.linearVelocity.y, newInput.z * playerSpeed);

        rb.linearVelocity = newVelocity;
    }
    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + input.y * camForward;
    }

    public void SprintPressed()
    {
        isSprinting = true;
        Debug.Log(isSprinting);
    }

    public void SprintReleased()
    {
        isSprinting = false;
        Debug.Log(isSprinting);
    }
}
