//using UnityEngine;

//[RequireComponent(typeof(PlayerMovement))]

//public class PlayerController : MonoBehaviour
//{
//    public float sprintSpeed = 10f;
//    public float crouchSpeed = 2.5f;
//    public float jumpForce = 8f;
//    public float gravity = 30f;
//    public float crouchHeight = 0.5f;
//    public float standingHeight = 2f;

//    // Stamina related variables
//    public float maxStamina = 100f;
//    public float staminaRegenRate = 10f;
//    private float currentStamina;

//    private bool isSprinting = false;
//    private bool isCrouching = false;

//    private Vector3 playerVelocity;
//    private Rigidbody rb;

//    [SerializeField]
//    private float speed = 5f;
//    [SerializeField]
//    private float lookSensitivity = 3f;

//    private PlayerMovement movement;

//    void Start()
//    {
//        movement = GetComponent<PlayerMovement>();
//        rb = GetComponent<Rigidbody>();
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

//        // Disable rigidbody gravity and rotation as we're handling it manually
//        rb.freezeRotation = true;
//        rb.useGravity = false;

//        // Initialize stamina
//        currentStamina = maxStamina;
//    }

//    void Update()
//    {
//        float _xMov = Input.GetAxisRaw("Horizontal");
//        float _zMov = Input.GetAxisRaw("Vertical");

//        Vector3 _movHorizontal = transform.right * _xMov;
//        Vector3 _movVertical = transform.forward * _zMov;

//        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

//        movement.Move(_velocity);

//        float _yRot = Input.GetAxisRaw("Mouse X");
//        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

//        movement.Rotate(_rotation);

//        float _xRot = Input.GetAxisRaw("Mouse Y");
//        float _cameraRotationX = _xRot * lookSensitivity;

//        movement.RotateCamera(_cameraRotationX);

//        HandleMovementInput();
//        HandleMouseLook();
//    }

//    void HandleMovementInput()
//    {
//        float horizontal = Input.GetAxis("Horizontal");
//        float vertical = Input.GetAxis("Vertical");

//        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

//        playerVelocity.y += gravity * Time.deltaTime;

//        // Check for sprint input and available stamina
//        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
//        float currentSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : speed;

//        if (isSprinting)
//        {
//            // Deplete stamina while sprinting
//            currentStamina -= Time.deltaTime;
//            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
//        }
//        else
//        {
//            // Regenerate stamina
//            currentStamina += staminaRegenRate * Time.deltaTime;
//            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
//        }

//        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
//        {
//            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
//        }

//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            isCrouching = !isCrouching;
//            transform.localScale = isCrouching ? new Vector3(1, crouchHeight, 1) : new Vector3(1, standingHeight, 1);
//            currentSpeed = isCrouching ? crouchSpeed : speed;
//        }

//        Vector3 move = transform.TransformDirection(direction) * currentSpeed;
//        rb.MovePosition(rb.position + move * Time.deltaTime);
//    }

//    void HandleMouseLook()
//    {
//        float mouseX = Input.GetAxis("Mouse X");
//        float mouseY = -Input.GetAxis("Mouse Y");

//        transform.Rotate(Vector3.up * mouseX);

//        float verticalRotation = transform.eulerAngles.x + mouseY;
//        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

//        transform.eulerAngles = new Vector3(verticalRotation, transform.eulerAngles.y, 0f);
//    }

//    bool IsGrounded()
//    {
//        // You may need to adjust the y position based on your player's collider
//        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
//    }
//}