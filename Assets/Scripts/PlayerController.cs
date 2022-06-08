using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private new AnimationScript animation;
    public Transform CherrySpawnPosition;
    public GameObject cherryPrefab;
    private CherrySpawner cherrySpawner;



    private Vector3 velocity;

    private bool isIdle = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isJumping = false; 
    
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;

    private Vector3 targetTransform;

    public int iteration = 10;
    [Range(0, 1)]
    public float weight = 1.0f;

    public float angleLimit = 90.0f;
    public float distanceLimit = 1.5f;

    private RaycastHit hit;
    private Ray ray;

    private Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = targetTransform - raycastOrigin.position;
        Vector3 aimDirection = raycastOrigin.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return raycastOrigin.position + direction;
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animation = GetComponent<AnimationScript>();
        cherrySpawner = GetComponent<CherrySpawner>();
    }

    private void Update()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");

        if (velocity.magnitude < 0.1 && !isIdle)
        {
            animation.Idle();
            isIdle = true;
            isWalking = false;
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animation.Walk();
            isIdle = false;
            isWalking = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isRunning)
        {
            isIdle = false;
            isWalking = false;
            isRunning = true;
            animation.Run();
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerMovement.Grounded())
        {
            isJumping = true;
            animation.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cherrySpawner.Fire();
            GameObject.Find("GameManager").GetComponent<GameManager>().CherryUpdate(-1);
        }
    }
        private void FixedUpdate()
        {
            playerMovement.Move(velocity.normalized, isWalking, isRunning, isJumping);
            isJumping = false;
        }
    
}
