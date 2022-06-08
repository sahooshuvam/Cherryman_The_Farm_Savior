using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed;
    [Range(0, 1)][SerializeField] private float walkSpeed;
    [Range(1, 2)][SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private bool isGrounded;


    private CharacterController characterController;

    private Vector3 jumpForceVelocity;


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

    public void Move(Vector3 velocity, bool isCrouching, bool isRunning, bool isJumping)
    {
        velocity = transform.forward * velocity.z + transform.right * velocity.x;

        if (isCrouching) characterController.Move(velocity * characterSpeed * walkSpeed * Time.deltaTime);
        else if (isCrouching) characterController.Move(velocity * characterSpeed * runSpeed * Time.deltaTime);
        else characterController.Move(velocity * characterSpeed * Time.deltaTime);

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
            GameObject.Find("GameManager").GetComponent<GameManager>().CherryUpdate(5);
            Debug.Log("Collecting Cherry Animation is happing");
            Destroy(collision.gameObject);
        }
    }

    public void TakeHit()
    {
        int damageAmount = -5;
        GameObject.Find("GameManager").GetComponent<GameManager>().healthUpdate(damageAmount);

    }
    public bool Grounded()
    {
        return isGrounded;
    }
}
