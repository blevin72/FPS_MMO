using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovementCC : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private CharacterController characterController;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Get a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Get a rotational vector for the camera
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void Update()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            Vector3 worldVelocity = transform.TransformDirection(velocity);

            characterController.Move(worldVelocity * Time.deltaTime);
        }
    }

    void PerformRotation()
    {
        transform.Rotate(rotation * Time.deltaTime);

        if (cam != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            // Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}

