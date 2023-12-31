using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool Sprint => Input.GetKey(KeyCode.LeftShift);
    private bool ShouldJump => Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded;

    [Header("Move Parameters")]
    [SerializeField] private float walkSpeed = 8.0f;
    [SerializeField] private float sprintSpeed = 15.0f;
    [SerializeField] private float crouchSpeed = 3.0f;

    private float walkValue;
    private float sprintValue;

    [Header("Mouse Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeed = 2.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpHeight = 5.0f;

    [Header("Stamina Parameters")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 10f;
    private float currentStamina;

    [Header("Audio")]
    [SerializeField] private AudioClip[] concreteSurface;
    [SerializeField] private AudioClip[] metalSurface;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crouchSound;

    [HideInInspector] public bool isCrouching, isJumping, isZooming;

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
        HandleMovementInput();
        HandleMouseLook();

        if (!isCrouching)
        {
            HandleJump();
        }

        ApplyFinalMovement();
        Crouch();

        if (Input.GetMouseButtonDown(1))
        {
            isZooming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isZooming = false;
        }

        // Manage stamina
        ManageStamina();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the raw input for later use
        Vector2 rawInput = new Vector2(horizontal, vertical);

        // Normalize to ensure diagonal movement is not faster
        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);
        moveDirection.Normalize();

        // Convert the move direction relative to the player's local space
        moveDirection = transform.TransformDirection(moveDirection);

        // Apply the movement to the character controller
        characterController.Move(moveDirection * (Sprint ? sprintSpeed : walkSpeed) * Time.deltaTime);

        // Calculate the currentInput based on raw input
        if (!isCrouching)
        {
            currentInput = new Vector2((Sprint ? sprintSpeed : walkSpeed) * rawInput.y,
                (Sprint ? sprintSpeed : walkSpeed) * rawInput.x);
        }
        else
        {
            currentInput = new Vector2((Sprint ? crouchSpeed : crouchSpeed) * rawInput.y,
                (Sprint ? crouchSpeed : crouchSpeed) * rawInput.x);
        }

        if (isZooming)
        {
            currentInput = new Vector2((Sprint ? crouchSpeed * 2 : crouchSpeed * 2) * rawInput.y,
                (Sprint ? crouchSpeed * 2 : crouchSpeed * 2) * rawInput.x);
        }

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
                         (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
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
        if (ShouldJump && characterController.isGrounded)
        {
            JumpAudio();
            moveDirection.y = jumpHeight;
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    private void ApplyFinalMovement()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y = -2f; // Adjust this value based on your needs
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Crouch()
    {
        float Perc1 = currentLerpTime1 / lerpTime;
        float Perc2 = currentLerpTime2 / lerpTime;
        if (Input.GetKeyDown(KeyCode.C))
        {
            CrouchAudio();
            if (isCrouching)
            {
                isCrouching = false;
            }
            else
            {
                isCrouching = true;
            }
        }

        if (isCrouching && mainCamera.transform.localPosition.y > -2f)
        {
            currentLerpTime2 = 0;
            currentLerpTime1 += Time.deltaTime;
            mainCamera.transform.localPosition = Vector3.Lerp(cameraStartPos, cameraEndPos, Perc1);
        }

        if (!isCrouching && mainCamera.transform.localPosition.y < 0f)

        {
            currentLerpTime1 = 0;
            currentLerpTime2 += Time.deltaTime;
            mainCamera.transform.localPosition = Vector3.Lerp(cameraEndPos, cameraStartPos, Perc2);
        }
    }

    private void ManageStamina()
    {
        if (Sprint && currentStamina > 0)
        {
            currentStamina -= Time.deltaTime;
        }
        else if (!Sprint && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    public void Footsteps()
    {
        RaycastHit hit = new RaycastHit();
        string floortag;
        if (characterController.isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                floortag = hit.collider.gameObject.tag;
                if (floortag == "Concrete")
                {
                    mainSound.clip = concreteSurface[Random.Range(0, concreteSurface.Length)];
                    mainSound.Play();
                }
                else if (floortag == "Metal")
                {
                    mainSound.clip = metalSurface[Random.Range(0, metalSurface.Length)];
                    mainSound.Play();
                }
            }
        }
    }

    public void JumpAudio()
    {
        mainSound.PlayOneShot(jumpSound, 0.6f);
    }

    public void CrouchAudio()
    {
        mainSound.PlayOneShot(crouchSound, 0.6f);
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
}




























//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    private bool Sprint => Input.GetKey(KeyCode.LeftShift);
//    private bool ShouldJump => Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded;

//    [Header("Move Parameters")]
//    [SerializeField] private float walkSpeed = 8.0f;
//    [SerializeField] private float sprintSpeed = 15.0f;
//    [SerializeField] private float crouchSpeed = 3.0f;

//    private float walkValue;
//    private float sprintValue;

//    [Header("Mouse Parameters")]
//    [SerializeField, Range(1, 10)] private float lookSpeed = 2.0f;

//    [Header("Jumping Parameters")]
//    [SerializeField] private float jumpHeight = 5.0f;

//    [Header("Stamina Parameters")]
//    [SerializeField] private float maxStamina = 100f;
//    [SerializeField] private float staminaRegenRate = 10f;
//    private float currentStamina;

//    [Header("Audio")]
//    [SerializeField] private AudioClip[] concreteSurface;
//    [SerializeField] private AudioClip[] metalSurface;
//    [SerializeField] private AudioClip jumpSound;
//    [SerializeField] private AudioClip crouchSound;

//    [HideInInspector] public bool isCrouching, isJumping, isZooming;

//    private CharacterController characterController;
//    private AudioSource mainSound;
//    private Vector3 moveDirection;
//    private Vector2 currentInput;
//    private float gravity = -9.81f;
//    private float rotationX = 0;
//    private Camera mainCamera;
//    private Vector3 cameraStartPos;
//    private Vector3 cameraEndPos;
//    private float cameraMaxPosY = -2f;
//    private float lerpTime = 0.2f;
//    private float currentLerpTime1;
//    private float currentLerpTime2;

//    private void Start()
//    {
//        GetReferences();
//    }

//    private void Update()
//    {
//        HandleMovementInput();
//        HandleMouseLook();

//        if (!isCrouching)
//        {
//            HandleJump();
//        }

//        ApplyFinalMovement();
//        Crouch();

//        if (Input.GetMouseButtonDown(1))
//        {
//            isZooming = true;
//        }
//        else if (Input.GetMouseButtonUp(1))
//        {
//            isZooming = false;
//        }

//        // Manage stamina
//        ManageStamina();
//    }

//    private void HandleMovementInput()
//    {
//        if (!isCrouching)
//        {
//            currentInput = new Vector2((Sprint ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"),
//                (Sprint ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
//        }
//        else
//        {
//            currentInput = new Vector2((Sprint ? crouchSpeed : crouchSpeed) * Input.GetAxis("Vertical"),
//                (Sprint ? crouchSpeed : crouchSpeed) * Input.GetAxis("Horizontal"));
//        }

//        if (isZooming)
//        {
//            currentInput = new Vector2((Sprint ? crouchSpeed * 2 : crouchSpeed * 2) * Input.GetAxis("Vertical"),
//                (Sprint ? crouchSpeed * 2 : crouchSpeed * 2) * Input.GetAxis("Horizontal"));
//        }

//        float moveDirectionY = moveDirection.y;
//        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
//                         (transform.TransformDirection(Vector3.right) * currentInput.y);
//        moveDirection.y = moveDirectionY;
//    }

//    private void HandleMouseLook()
//    {
//        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
//        rotationX = Mathf.Clamp(rotationX, -80, 80);
//        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
//        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
//    }

//    private void HandleJump()
//    {
//        if (ShouldJump)
//        {
//            JumpAudio();
//            moveDirection.y = jumpHeight;
//            isJumping = true;
//        }
//        else
//        {
//            isJumping = false;
//        }
//    }

//    private void ApplyFinalMovement()
//    {
//        if (!characterController.isGrounded)
//        {
//            moveDirection.y += gravity * Time.deltaTime;
//            characterController.Move(moveDirection * Time.deltaTime);
//        }
//    }

//    private void Crouch()
//    {
//        float Perc1 = currentLerpTime1 / lerpTime;
//        float Perc2 = currentLerpTime2 / lerpTime;
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            CrouchAudio();
//            if (isCrouching)
//            {
//                isCrouching = false;
//            }
//            else
//            {
//                isCrouching = true;
//            }
//        }

//        if (isCrouching && mainCamera.transform.localPosition.y > -2f)
//        {
//            currentLerpTime2 = 0;
//            currentLerpTime1 += Time.deltaTime;
//            mainCamera.transform.localPosition = Vector3.Lerp(cameraStartPos, cameraEndPos, Perc1);
//        }

//        if (!isCrouching && mainCamera.transform.localPosition.y < 0f)

//        {
//            currentLerpTime1 = 0;
//            currentLerpTime2 += Time.deltaTime;
//            mainCamera.transform.localPosition = Vector3.Lerp(cameraEndPos, cameraStartPos, Perc2);
//        }
//    }

//    private void ManageStamina()
//    {
//        if (Sprint && currentStamina > 0)
//        {
//            currentStamina -= Time.deltaTime;
//        }
//        else if (!Sprint && currentStamina < maxStamina)
//        {
//            currentStamina += staminaRegenRate * Time.deltaTime;
//        }

//        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
//    }

//    public void Footsteps()
//    {
//        RaycastHit hit = new RaycastHit();
//        string floortag;
//        if (characterController.isGrounded)
//        {
//            if (Physics.Raycast(transform.position, Vector3.down, out hit))
//            {
//                floortag = hit.collider.gameObject.tag;
//                if (floortag == "Concrete")
//                {
//                    mainSound.clip = concreteSurface[Random.Range(0, concreteSurface.Length)];
//                    mainSound.Play();
//                }
//                else if (floortag == "Metal")
//                {
//                    mainSound.clip = metalSurface[Random.Range(0, metalSurface.Length)];
//                    mainSound.Play();
//                }
//            }
//        }
//    }

//    public void JumpAudio()
//    {
//        mainSound.PlayOneShot(jumpSound, 0.6f);
//    }

//    public void CrouchAudio()
//    {
//        mainSound.PlayOneShot(crouchSound, 0.6f);
//    }

//    public void GetReferences()
//    {
//        characterController = GetComponent<CharacterController>();
//        mainCamera = GetComponentInChildren<Camera>();
//        cameraEndPos = mainCamera.transform.localPosition + Vector3.up * cameraMaxPosY;
//        mainSound = GetComponent<AudioSource>();
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

//        // Initialize stamina
//        currentStamina = maxStamina;
//    }
//}