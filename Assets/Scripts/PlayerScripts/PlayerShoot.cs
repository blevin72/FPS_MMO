using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    private const string ZOMBIE_TAG = "Zombie";

    public PlayerWeapon weapon;
    public int attackDamage = 30;
    public float soundIntensity = 5f;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;

    [SerializeField]
    private Camera cam;

    private void Start()
    {
        if (isLocalPlayer)
        {
            // Find the Camera component in the parent hierarchy
            cam = GetComponentInParent<Camera>();

            if (cam == null)
            {
                Debug.LogError("Player Shoot: No camera referenced");
                this.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            CmdShoot();
        }
    }

    [Command]
    private void CmdShoot()
    {
        if (spherecastSpawn == null)
        {
            Debug.Log("spherecastSpawn is not assigned.");
            return;
        }

        // SphereCast to find zombies within the specified layer and range
        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, LayerMask.GetMask(ZOMBIE_TAG));
        for (int i = 0; i < zombies.Length; i++)
        {
            // Check if the hit object has the ZombieAI component
            ZombieAI zombieAI = zombies[i].GetComponent<ZombieAI>();
            if (zombieAI != null)
            {
                // Deal damage to the zombie
                zombieAI.CmdTakeDamage(attackDamage);
            }
        }

        // Perform a SphereCast to detect hits
        RaycastHit hit;
        if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.forward, out hit, zombieLayer))
        {
            // Check if the hit object has the ZombieAI component
            ZombieAI zombieAI = hit.transform.GetComponent<ZombieAI>();
            if (zombieAI != null)
            {
                // Deal damage to the zombie
                zombieAI.CmdTakeDamage(attackDamage);
            }
        }
    }
}





















//using UnityEngine;
//using Mirror;

//public class PlayerShoot : NetworkBehaviour
//{
//    private const string PLAYER_TAG = "Player";

//    public PlayerWeapon weapon;
//    public int attackDamage = 30;
//    public float soundIntensity = 5f;
//    public Transform spherecastSpawn;
//    public LayerMask zombieLayer;

//    [SerializeField]
//    private Camera cam;

//    private void Start()
//    {
//        if (isLocalPlayer)
//        {
//            // Find the Camera component in the parent hierarchy
//            cam = GetComponentInParent<Camera>();

//            if (cam == null)
//            {
//                Debug.LogError("Player Shoot: No camera referenced");
//                this.enabled = false;
//            }
//        }
//    }

//    private void Update()
//    {
//        if (isLocalPlayer && Input.GetMouseButtonDown(0))
//        {
//            CmdShoot();
//        }
//    }

//    [Command]
//    private void CmdShoot()
//    {
//        if (spherecastSpawn == null)
//        {
//            Debug.Log("spherecastSpawn is not assigned.");
//            return;
//        }

//        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
//        for (int i = 0; i < zombies.Length; i++)
//        {
//            zombies[i].GetComponent<ZombieAI>().CmdOnHit(attackDamage);
//        }

//        RaycastHit hit;
//        if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.forward, out hit, zombieLayer))
//        {
//            hit.transform.GetComponent<ZombieAI>().CmdOnHit(attackDamage);
//        }
//    }
//}


//public class PlayerShoot : NetworkBehaviour
//{
//    private const string PLAYER_TAG = "Player";

//    public PlayerWeapon weapon;
//    public int attackDamage = 30;
//    public float soundIntensity = 5f;
//    public Transform spherecastSpawn;
//    public LayerMask zombieLayer;

//    [SerializeField]
//    private Camera cam;

//    [SerializeField]
//    private LayerMask mask;

//    private void Start()
//    {
//        if (cam == null)
//        {
//            GameObject playerCamera = GameObject.FindGameObjectWithTag("Player");
//            if (playerCamera != null)
//            {
//                cam = playerCamera.GetComponent<Camera>();
//            }

//            if (cam == null)
//            {
//                Debug.LogError("Player Shoot: No camera referenced");
//                this.enabled = false;
//            }
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Shoot();
//        }
//    }

//    [Command]
//    private void Shoot()
//    {
//        if (spherecastSpawn == null)
//        {
//            Debug.Log("spherecastSpawn is not assigned.");
//        }

//        //animator.SetTrigger("Fire1");
//        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
//        for (int i = 0; i < zombies.Length; i++)
//        {
//            zombies[i].GetComponent<ZombieAI>().OnAware();
//        }
//        RaycastHit hit;
//        if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, zombieLayer))
//        {
//            hit.transform.GetComponent<ZombieAI>().OnHit(attackDamage);
//        }

//        //RaycastHit _hit;
//        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
//        //{
//        //if (_hit.collider.tag == PLAYER_TAG)
//        //{
//        //CmdPlayerShot(_hit.collider.name, weapon.damage);
//        //_hit.transform.GetComponent<ZombieAI>().OnHit(attackDamage);
//        //}
//        //}


//        //[Command]
//        //void CmdPlayerShot(string _playerID, int _damage)
//        //{
//        //Debug.Log(_playerID + " has been shot.");
//        //Player _player = GameManager.GetPlayer(_playerID);
//        //_player.RpcTakeDamage(_damage);
//        //}
//    }
//}
