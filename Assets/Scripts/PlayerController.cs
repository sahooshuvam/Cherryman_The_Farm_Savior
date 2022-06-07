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

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Spider")
            {
                Debug.Log(hit.collider.name);
                GameObject gameObject = Instantiate(cherryPrefab, ray.origin, Quaternion.identity);
                gameObject.GetComponent<Cherry>().MoveDirection(GetTargetPosition(),hit.transform.position);

            }
        }


        if (Input.GetButton("Fire1"))
        {
            animation.Attack();
            SpiderGotHit();
        }
    }

    private void SpiderGotHit()
    {
        Ray ray;
        RaycastHit hitInfo;
        if (Physics.Raycast(CherrySpawnPosition.position, CherrySpawnPosition.forward, out hitInfo, 100f))
        {
            GameObject hitInsect = hitInfo.collider.gameObject;
            if (hitInsect.CompareTag("Spider"))
            {
                Debug.Log("Its Hit Insect");
            }
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(velocity.normalized, isWalking, isRunning, isJumping) ;
        isJumping = false;
    }

}
