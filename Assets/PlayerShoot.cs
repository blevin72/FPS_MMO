using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if(cam == null)
        {
            GameObject playerCamera = GameObject.FindGameObjectWithTag("Player");
            if(playerCamera != null)
            {
                cam = playerCamera.GetComponent<Camera>();
            }

            if(cam == null)
            {
                Debug.LogError("PLayer Shoot: No camera referenced");
                this.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            //we hit something
            Debug.Log("We hit" + _hit.collider.name);
        }
    }    
}
