using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //_xMov is movement on the x axis (left/right)
        //_zMov is movement on the z axis (forward/backward)
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //apply movement
        movement.Move(_velocity);

        //Calculate movement as a 3D vector: turning around
        float _yRot = Input.GetAxisRaw("Mouse X"); //when moving mouse on x axis it will rotate player on the y axis
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //apply rotation
        movement.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector: looking up & down
        float _xRot = Input.GetAxisRaw("Mouse Y"); //when moving mouse on x axis it will rotate player on the y axis
        float _cameraRotationX = _xRot * lookSensitivity;

        //apply camera rotation
        movement.RotateCamera(_cameraRotationX);
    }
}

