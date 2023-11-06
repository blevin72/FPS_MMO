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
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public Transform[] waypoints;

    private GameObject spawnedPlayer;
    private bool isAware = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int wayPointIndex = 0;
    private Animator animator;

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
        Detect();
    }

    public void Detect()
    {
        if (isAware == true)
        {
            agent.SetDestination(fpsc.transform.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
        }
        else
        {
            SearchForPlayer();
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
                }
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 0.5f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}
