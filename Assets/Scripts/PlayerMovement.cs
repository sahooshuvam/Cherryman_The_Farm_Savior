using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC VARIABLE
    public CharacterController controller;
    public GameObject cherryPrefaB;
    public float playerWalkSpeed = 6f;
    public float playerRunSpeed = 12f;
    public GameObject cherryPrefab;
    public Transform thrownPosition;
    #endregion

    #region PRIVATE VARIABLE
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
       //CharacterController controller = GetComponent<CharacterController>();   
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetTrigger("IsIdle");
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetTrigger("IsWalking");
            PlayerWalk(xInput, zInput);
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("IsThrowing");
        }
    }

    private void PlayerWalk(float xInput, float zInput)
    {
        Vector3 move = transform.right * xInput + transform.forward * zInput;
        controller.Move(move * playerWalkSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollectedCherry")
        {
            Debug.Log("It's a collected cherry");
            animator.SetTrigger("IsCollecting");
        }
    }

    public void CherryThrown()
    {
        Instantiate(cherryPrefab, thrownPosition.position, Quaternion.identity);
    }
}
