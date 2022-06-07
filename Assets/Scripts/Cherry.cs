using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveDirection(Vector3 CherryDirections,Vector3 hitPosition)
    {

        transform.LookAt(hitPosition);
        rb.velocity = transform.forward * speed;
    }
}
