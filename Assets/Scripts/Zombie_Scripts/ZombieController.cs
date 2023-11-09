using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{

    [SerializeField]
    public float stoppingDistance = 3;

    private NavMeshAgent agent = null;
    private Animator anim = null;

    [SerializeField]
    public Transform player;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        agent.destination = player.position;
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToPlayer();

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= agent.stoppingDistance) 
        {
            anim.SetFloat("Speed", 0f);
        }
    }

    private void RotateToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

}