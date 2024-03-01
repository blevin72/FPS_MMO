using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public GameObject handGunAudioPrefab;
    public int attackDamage = 30;
    public float soundIntensity = 5f;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;

    private AudioSource handGunAudioSource;
    private const string ZOMBIE_TAG = "Zombie";
    private Camera cam;

    private void Start()
    {
        if (isLocalPlayer)
        {
            cam = GetComponentInChildren<Camera>();
            if (cam == null)
            {
                Debug.LogError("Player Shoot: No camera referenced");
                this.enabled = false;
            }
        }

        GetReferences();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            ShootGun();
        }
    }

    [Command]
    void ShootGun()
    {
        // Instantiate the audio prefab
        GameObject audioObject = Instantiate(handGunAudioPrefab, transform.position, Quaternion.identity);
        handGunAudioSource = audioObject.GetComponent<AudioSource>();
        handGunAudioSource.Play();
        HandGunAudioCheck();

        if (!isLocalPlayer)
            return;

        // Perform a SphereCast to detect hits
        RaycastHit hit;
        if (Physics.SphereCast(spherecastSpawn.position, 0.5f, cam.transform.forward, out hit, Mathf.Infinity, zombieLayer))
        {
            // Check if the hit object has the ZombieAI component
            ZombieAI zombieAI = hit.transform.GetComponent<ZombieAI>();
            if (zombieAI != null)
            {
                // Deal damage to the zombie
                zombieAI.CmdTakeDamage(attackDamage);
            }
        }

        // Destroy the audio object after playing
        Destroy(audioObject, handGunAudioSource.clip.length);
    }

    void GetReferences()
    {
        handGunAudioSource = handGunAudioPrefab.GetComponent<AudioSource>();
    }

    void HandGunAudioCheck()
    {
        if (handGunAudioSource != null)
        {
            handGunAudioSource.Play();
        }
        else
        {
            Debug.LogError("Handgun audio source is not assigned.");
            return;
        }
    }
}