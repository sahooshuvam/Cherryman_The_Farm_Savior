using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private new Transform camera;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        ray.origin = camera.transform.position;
        ray.direction = camera.transform.forward;

        Physics.Raycast(ray, out hit);
        transform.position = hit.point;
    }
}
