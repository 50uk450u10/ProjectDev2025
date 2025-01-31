using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] float speed;
    PlayerInput playerMovement;
    IA_Player movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerInput>();
        movement = new IA_Player();
        movement.PlayerMovement.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = movement.PlayerMovement.Movement.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        var newInput = GetCameraBasedInput(input, Camera.main);

        var newVelocity = new Vector3(newInput.x * speed, rb.linearVelocity.y, newInput.z * speed);

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
}
