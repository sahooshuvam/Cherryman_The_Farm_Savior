using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform CameraOrigin;
    [SerializeField] float turnSpeed;
    [SerializeField] float verticalMinRotation;
    [SerializeField] float verticalMaxRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += mouseInput.x * turnSpeed;
        transform.rotation = Quaternion.Euler(currentRotation);

        if (CameraOrigin != null)
        {
            currentRotation = CameraOrigin.localRotation.eulerAngles;
            currentRotation.x -= mouseInput.y * turnSpeed;
            if (currentRotation.x > 180)
                currentRotation.x -= 360;
            currentRotation.x = Mathf.Clamp(currentRotation.x, verticalMinRotation, verticalMaxRotation);
            CameraOrigin.localRotation = Quaternion.Euler(currentRotation);
        }
    }
}
