using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PUBLIC VARIABLE
    public Transform CherrySpawnPosition;
    public GameObject cherryPrefab;
    public int iteration = 10;
    [Range(0, 1)] public float weight = 1.0f;
    public float distanceLimit = 1.5f;
    public float angleLimit = 90.0f;
    #endregion

    #region PRIVATE VARIABLE
    private PlayerMovement playerMovement;
    private AnimationScript animation;
    private CherrySpawner cherrySpawner;
    private Vector3 velocity;
    private Vector3 targetTransform;
    private Ray ray;
    private RaycastHit hit;
    private bool isIdle = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isJumping = false; 
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;
    #endregion

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

        if (velocity.magnitude < 0.1 && !isIdle) //By default Cherryman is in Idle 
        {
            animation.Idle();
            isIdle = true;
            isWalking = false;
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // For Cherryman Walking
        {
            animation.Walk();
            isIdle = false;
            isWalking = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isRunning) // For Cherryman Running 
        {
            isIdle = false;
            isWalking = false;
            isRunning = true;
            animation.Run();
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerMovement.Grounded()) // Cherryman Jump
        {
            isJumping = true;
            animation.Jump();
        }

        if (Input.GetMouseButtonDown(0)) // on Mouse Left click Cherry will Spawn
        {
            animation.Attack();
            //GameObject.Find("GameManager").GetComponent<GameManager>().CherryUpdate(-1);
            GameManager.Instance.CherryUpdate(-1);

        }
    }
        private void FixedUpdate()
        {
            playerMovement.Move(velocity.normalized, isWalking, isRunning, isJumping);
            isJumping = false;
        }
    
}
