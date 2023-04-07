using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float jumpHeight = 2f;

    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1;

        // Face Forvard
        transform.forward = new Vector3(horizontalInput, 0, MathF.Abs(horizontalInput) - 1);

        // IsGrounded
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            // add gravity
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += MathF.Sqrt(jumpHeight * -2 * gravity);
        }

        // Vertical Velocity
        characterController.Move(velocity * Time.deltaTime);
    }
}
