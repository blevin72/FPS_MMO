using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class ZombieAI : MonoBehaviour
{
    public enum WanderType { Random, Waypoint };
    public GameObject fpsc;
    public WanderType wanderType = WanderType.Random;
    public int health = 100;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public float loseThreshold = 10f;
    public Transform[] waypoints;

    private GameObject spawnedPlayer;
    private bool isAware = false;
    private bool isDetecting = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int wayPointIndex = 0;
    private Animator animator;
    private float loseTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayer();
        fpsc = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            agent.speed = 0;
            animator.enabled = false;
            return;
        }

        Detect();
        SearchForPlayer();
    }

    public void Detect()
    {
        if (isAware == true)
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

    public void AssignPlayer()
    {
        if (fpsc != null)
        {
            spawnedPlayer = Instantiate(fpsc, transform.position, transform.rotation);
            NetworkServer.Spawn(spawnedPlayer);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned!");
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, fpsc.transform.position, out hit, -1))
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
    }

    public void OnAware()
    {
        isAware = true;
        isDetecting = true;
        loseTimer = 0;
    }

    public void Wander()
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
                    if (wayPointIndex == waypoints.Length -1)
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

    public void OnHit(int damage)
    {
        health -= damage;
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}
