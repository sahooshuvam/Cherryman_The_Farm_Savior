using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed;
    [Range(0, 1)][SerializeField] private float crouchSpeed;
    [Range(1, 2)][SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private bool isGrounded;


    private CharacterController characterController;

    private Vector3 jumpForceVelocity;

    private float turnSmoothVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && jumpForceVelocity.y < 0)
        {
            jumpForceVelocity.y = 0f;
        }
    }

    public void Move(Vector3 velocity,bool isWalking, bool isRunning, bool isJumping)
    {
        velocity = transform.forward * velocity.z + transform.right * velocity.x;

        if (isWalking) 
            characterController.Move(velocity * characterSpeed * crouchSpeed * Time.deltaTime);

       
        else
            characterController.Move(velocity * 2 * characterSpeed * Time.deltaTime);

        Jump(isJumping);
    }

    private void Jump(bool isJumping)
    {
        if (isJumping && isGrounded)
        {
            jumpForceVelocity.y = jumpForce * Time.deltaTime;
        }

        jumpForceVelocity.y += gravity * Time.deltaTime;
        characterController.Move(jumpForceVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollectedCherry"))
        {
            gameObject.GetComponent<AnimationScript>().CollectingCherry();
            Destroy(collision.gameObject);
        }
    }

    public bool Grounded()
    {
        return isGrounded;
    }
}
