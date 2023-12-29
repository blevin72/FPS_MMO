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