using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;
    public int attackDamage = 30;
    public float soundIntensity = 5f;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if (cam == null)
        {
            GameObject playerCamera = GameObject.FindGameObjectWithTag("Player");
            if (playerCamera != null)
            {
                cam = playerCamera.GetComponent<Camera>();
            }

            if (cam == null)
            {
                Debug.LogError("Player Shoot: No camera referenced");
                this.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        if (spherecastSpawn == null)
        {
            Debug.Log("spherecastSpawn is not assigned.");
        }

        //animator.SetTrigger("Fire1");
        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].GetComponent<ZombieAI>().OnAware();
        }
        RaycastHit hit;
        if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, zombieLayer))
        {
            hit.transform.GetComponent<ZombieAI>().OnHit(attackDamage);
        }

        //RaycastHit _hit;
        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        //{
        //if (_hit.collider.tag == PLAYER_TAG)
        //{
        //CmdPlayerShot(_hit.collider.name, weapon.damage);
        //_hit.transform.GetComponent<ZombieAI>().OnHit(attackDamage);
        //}
        //}


        //[Command]
        //void CmdPlayerShot(string _playerID, int _damage)
        //{
        //Debug.Log(_playerID + " has been shot.");
        //Player _player = GameManager.GetPlayer(_playerID);
        //_player.RpcTakeDamage(_damage);
        //}
    }
}
