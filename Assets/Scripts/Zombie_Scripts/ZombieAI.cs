using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class ZombieAI : NetworkBehaviour
{
    public enum WanderType { Random, Waypoint };
    public WanderType wanderType = WanderType.Random;
    public int health = 100;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public float loseThreshold = 10f;
    public Transform[] waypoints;
    public LayerMask playerLayer;
    public GameObject fpsc;

    private bool isAware = false;
    private bool isDetecting = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Animator animator;
    private float loseTimer = 0;
    private int wayPointIndex = 0;

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    void Start()
    {
        fpsc = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragdollColliders)
        {
            if (!col.CompareTag("Zombie"))
            {
                col.enabled = false;
            }
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isServer) return;

        if (health <= 0)
        {
            Death();
            return;
        }

        SearchForPlayer();
    }

    public void CmdTakeDamage(int damage)
    {
        if (!isServer)
            return;

        // Apply damage to the zombie
        health -= damage;

        // Check if the zombie should die
        if (health <= 0)
        {
            RpcDie(); // Inform all clients that the zombie has died
            // You may want to add other logic for handling death, such as scoring or respawning.
        }
    }

    [ClientRpc]
    private void RpcDie()
    {
        // Perform death-related actions on all clients, e.g., play death animation, disable the zombie, etc.
        gameObject.SetActive(false);
    }

    void SearchForPlayer()
    {
        if (!isServer)
            return;

        if (fpsc != null)
        {
            if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
            {
                if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, fpsc.transform.position - transform.position, out hit, viewDistance, playerLayer))
                    {
                        if (hit.transform.CompareTag("Player"))
                        {
                            OnAware();
                        }
                        else
                        {
                            isDetecting = false;
                        }
                    }
                    else
                    {
                        isDetecting = false;
                    }
                }
                else
                {
                    isDetecting = false;
                }
            }
            else
            {
                isDetecting = false;
            }

            if (isAware)
            {
                agent.SetDestination(fpsc.transform.position);
                animator.SetBool("Aware", true);
                agent.speed = chaseSpeed;

                if (!isDetecting)
                {
                    loseTimer += Time.deltaTime;
                    if (loseTimer >= loseThreshold)
                    {
                        isAware = false;
                        loseTimer = 0;
                    }
                }
            }
            else
            {
                Wander();
                animator.SetBool("Aware", false);
                agent.speed = wanderSpeed;
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
        isDetecting = true;
        loseTimer = 0;
    }

    void Wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                wanderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else
        {
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[wayPointIndex].position, transform.position) < 2f)
                {
                    if (wayPointIndex == waypoints.Length - 1)
                    {
                        wayPointIndex = 0;
                    }
                    else
                    {
                        wayPointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[wayPointIndex].position);
                }
            }
            else
            {
                Debug.LogWarning("Please Assign more than 1 waypoint to Enemy.");
            }
        }
    }

    [Command]
    public void CmdOnHit(int damage)
    {
        OnHit(damage);
    }

    [Server]
    public void OnHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        RagDoll();
    }

    void RagDoll()
    {
        agent.speed = 0;
        animator.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using Mirror;

//public class ZombieAI : MonoBehaviour
//{
//    public enum WanderType { Random, Waypoint }; 
//    public WanderType wanderType = WanderType.Random;
//    public GameObject fpsc;
//    public int health = 100;
//    public float wanderSpeed = 4f;
//    public float chaseSpeed = 7f;
//    public float fov = 120f;
//    public float viewDistance = 10f;
//    public float wanderRadius = 7f;
//    public float loseThreshold = 10f;
//    public Transform[] waypoints;
//    public LayerMask playerLayer;

//    private GameObject spawnedPlayer;
//    private bool isAware = false;
//    private bool isDetecting = false;
//    private Vector3 wanderPoint;
//    private NavMeshAgent agent;
//    private int wayPointIndex = 0;
//    private Animator animator;
//    private float loseTimer = 0;

//    private Collider[] ragdollColliders;
//    private Rigidbody[] ragdollRigidbodies;

//    // Start is called before the first frame update
//    void Start()
//    {
//        AssignPlayer();
//        fpsc = GameObject.FindGameObjectWithTag("Player");
//        agent = GetComponent<NavMeshAgent>();
//        animator = GetComponentInChildren<Animator>();
//        wanderPoint = RandomWanderPoint();
//        ragdollColliders = GetComponentsInChildren<Collider>();
//        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
//        foreach (Collider col in ragdollColliders)
//        {
//            if (!col.CompareTag("Zombie"))
//            {
//                col.enabled = false;
//            }   
//        }
//        foreach (Rigidbody rb in ragdollRigidbodies)
//        {
//            rb.isKinematic = true;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (health <= 0)
//        {
//            Death();
//            return;
//        }
//        Detect();
//        SearchForPlayer();
//    }

//    public void Detect()
//    {
//        if (isAware == true)
//        {
//            agent.SetDestination(fpsc.transform.position);
//            animator.SetBool("Aware", true);
//            agent.speed = chaseSpeed;
//            if (!isDetecting)
//            {
//                loseTimer += Time.deltaTime;
//                if (loseTimer >= loseThreshold)
//                {
//                    isAware = false;
//                    loseTimer = 0;
//                }
//            }
//        }
//        else
//        {
//            Wander();
//            animator.SetBool("Aware", false);
//            agent.speed = wanderSpeed;
//        }
//    }

//    public void AssignPlayer()
//    {
//        if (fpsc != null)
//        {
//            spawnedPlayer = Instantiate(fpsc, transform.position, transform.rotation);
//            NetworkServer.Spawn(spawnedPlayer);
//        }
//        else
//        {
//            Debug.LogError("Player prefab is not assigned!");
//        }
//    }

//    public void SearchForPlayer()
//    {
//        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
//        {
//            if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
//            {
//                RaycastHit hit;
//                if (Physics.Linecast(transform.position, fpsc.transform.position, out hit, -1))
//                {
//                    if (hit.transform.CompareTag("Player"))
//                    {
//                        OnAware();
//                    }
//                    else
//                    {
//                        isDetecting = false;
//                    }
//                }
//                else
//                {
//                    isDetecting = false;
//                }
//            }
//            else
//            {
//                isDetecting = false;
//            }
//        }
//        else
//        {
//            isDetecting = false;
//        }
//    }

//    public void OnAware()
//    {
//        isAware = true;
//        isDetecting = true;
//        loseTimer = 0;
//    }

//    public void Wander()
//    {
//        if (wanderType == WanderType.Random)
//        {
//            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
//            {
//                wanderPoint = RandomWanderPoint();
//            }
//            else
//            {
//                agent.SetDestination(wanderPoint);
//            }
//        }
//        else
//        {
//            if (waypoints.Length >= 2)
//            {
//                if (Vector3.Distance(waypoints[wayPointIndex].position, transform.position) < 2f)
//                {
//                    if (wayPointIndex == waypoints.Length -1)
//                    {
//                        wayPointIndex = 0;
//                    }
//                    else
//                    {
//                        wayPointIndex++;
//                    }
//                }
//                else
//                {
//                    agent.SetDestination(waypoints[wayPointIndex].position);
//                }
//            }
//            else
//            {
//                Debug.LogWarning("Please Assign more than 1 waypoint to Enemy.");
//            }
//        }
//    }

//    public void OnHit(int damage)
//    {
//        health -= damage;
//    }

//    public void Death()
//    {
//        if (health <= 0)
//        {
//            RagDoll();
//            return;
//        }
//    }

//    public void RagDoll()
//    {
//        agent.speed = 0;
//        animator.enabled = false;
//        foreach (Collider col in ragdollColliders)
//        {
//            col.enabled = true;
//        }
//        foreach (Rigidbody rb in ragdollRigidbodies)
//        {
//            rb.isKinematic = false;
//        }
//    }

//    public Vector3 RandomWanderPoint()
//    {
//        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
//        NavMeshHit navHit;
//        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
//        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
//    }
//}


