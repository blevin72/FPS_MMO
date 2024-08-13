using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool Sprint => Input.GetKey(KeyCode.LeftShift);
    private bool ShouldJump => Input.GetKeyDown(KeyCode.Space) && isGrounded;

    [Header("Move Parameters")]
    [SerializeField] private float walkSpeed = 8.0f;
    [SerializeField] private float sprintSpeed = 15.0f;
    [SerializeField] private float crouchSpeed = 3.0f;

    [Header("Mouse Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeed = 2.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 5.0f;

    [Header("Stamina Parameters")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 10f;
    private float currentStamina;

    [Header("Audio")]
    [SerializeField] private AudioClip[] concreteSurface;
    [SerializeField] private AudioClip[] metalSurface;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crouchSound;

    [HideInInspector] public bool isCrouching, isJumping, isZooming, isGrounded; // Declare isGrounded here

    private CharacterController characterController;
    private AudioSource mainSound;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float gravity = -9.81f;
    private float rotationX = 0;
    private Camera mainCamera;
    private Vector3 cameraStartPos;
    private Vector3 cameraEndPos;
    private float cameraMaxPosY = -2f;
    private float lerpTime = 0.2f;
    private float currentLerpTime1;
    private float currentLerpTime2;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        // Check if the player is grounded using raycast
        isGrounded = CheckIfGrounded();

        HandleMovementInput();
        HandleMouseLook();

        if (!isCrouching)
        {
            HandleJump();
        }

        ApplyFinalMovement();
        Crouch();

        // Manage stamina
        ManageStamina();
    }

    private bool CheckIfGrounded()
    {
        // Cast a ray downwards to check if grounded
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f) &&
               hit.collider != null;
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the raw input for later use
        Vector2 rawInput = new Vector2(horizontal, vertical);

        // Normalize to ensure diagonal movement is not faster
        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Convert the move direction relative to the player's local space
        moveDirection = transform.TransformDirection(moveDirection);

        // Apply the movement to the character controller
        characterController.Move(moveDirection * (Sprint ? sprintSpeed : walkSpeed) * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -80, 80);
        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump && isGrounded)
        {
            JumpAudio();
            moveDirection.y = jumpForce; // Set the upward force for jumping
        }
    }

    private void ApplyFinalMovement()
    {
        // Apply gravity
        if (isGrounded)
        {
            moveDirection.y = -2f; // Keep player grounded slightly
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Crouch()
    {
        // Your existing crouch logic goes here
    }

    private void ManageStamina()
    {
        // Your existing stamina management logic goes here
    }

    public void GetReferences()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = GetComponentInChildren<Camera>();
        cameraEndPos = mainCamera.transform.localPosition + Vector3.up * cameraMaxPosY;
        mainSound = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize stamina
        currentStamina = maxStamina;
    }

    public void JumpAudio()
    {
        mainSound.PlayOneShot(jumpSound, 0.6f);
    }

    public void CrouchAudio()
    {
        mainSound.PlayOneShot(crouchSound, 0.6f);
    }
}
