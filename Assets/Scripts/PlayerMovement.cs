using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region PRIVATE VARIABLE
    [SerializeField] private float characterSpeed; // 
    [Range(0, 1)][SerializeField] private float walkSpeed; // cherryman walking speed
    [SerializeField] private float jumpForce; // cherryman jumping force
    [Range(1, 2)][SerializeField] private float runSpeed; // cherryman Running speed 
    [SerializeField] private float gravity; // cherryman gravity 
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private bool isGrounded;
    private CharacterController characterController; // Taking the reference of the instances
    private Vector3 jumpForceVelocity;
    #endregion


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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollectedCherry"))
        {
            gameObject.GetComponent<AnimationScript>().CollectingCherry();
           // GameObject.Find("GameManager").GetComponent<GameManager>().CherryUpdate(5);
            GameManager.Instance.CherryUpdate(5);
            Debug.Log("Collecting Cherry Animation is happing");
            Destroy(collision.gameObject);
        }
    }

    #region PUBLIC METHODS
    public void Move(Vector3 velocity, bool isWalking, bool isRunning, bool isJumping)
    {
        velocity = transform.forward * velocity.z + transform.right * velocity.x;

        if (isWalking) characterController.Move(velocity * characterSpeed * walkSpeed * Time.deltaTime);
        else if (isWalking) characterController.Move(velocity * characterSpeed * runSpeed * Time.deltaTime);
        else characterController.Move(velocity * characterSpeed * Time.deltaTime);

        Jump(isJumping);
    }

    public void TakeHit()
    {
        int damageAmount = -5;
        //GameObject.Find("GameManager").GetComponent<GameManager>().healthUpdate(damageAmount);
        GameManager.Instance.healthUpdate(damageAmount);
    }
    public bool Grounded()
    {
        return isGrounded;
    }

    #endregion

    #region PRIVATE METHODS

    private void Jump(bool isJumping)
    {
        if (isJumping && isGrounded)
        {
            jumpForceVelocity.y = jumpForce * Time.deltaTime;
        }

        jumpForceVelocity.y += gravity * Time.deltaTime;
        characterController.Move(jumpForceVelocity * Time.deltaTime);
    }

    #endregion

}
