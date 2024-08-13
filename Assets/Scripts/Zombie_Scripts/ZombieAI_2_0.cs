using System.Collections;
using UnityEngine;

public class ZombieAI_2_0 : MonoBehaviour
{
    public float FOV;
    public float detectionDistance;
    public float wanderRadius;
    public float playerLostTime;
    public float wanderSpeed;
    public float chaseSpeed;
    public float rotateSpeed;
    public float attackRange = 2f; //Distance from player to trigger attack
    public float attackCooldown = 1.5f; // Cooldown between attacks
    public int maxHealth = 100;

    private Transform player;
    private Animator animator;
    private float health;
    private Vector3 wanderTarget;
    private float playerLostTimer;
    private bool playerInFOV;
    private float lastAttackTime;
    private bool isChasing;

    // Attack Types
    private enum AttackType
    {
        LeftHand,
        RightHand,
        RightHand_2,
        LeftHand_2,
        //ChargedLeftHand
    }

    private AttackType nextAttack = AttackType.RightHand;

    // Zombie states
    private enum ZombieState
    {
        Wandering,
        Detecting,
        Chasing,
        Attacking
    }

    private ZombieState currentState = ZombieState.Wandering;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = players[0].transform; // Access the first player
        }
        animator = GetComponent<Animator>();
        health = maxHealth;
        SetWanderTarget();
    }

    private void Update()
    {
        switch (currentState)
        {
            case ZombieState.Wandering:
                Debug.Log("Zombie is Wandering");
                Wander();
                CheckPlayerInFOV();
                break;

            case ZombieState.Detecting:
                Debug.Log("Zombie is Detecting");
                break;

            case ZombieState.Chasing:
                Debug.Log("Zombie is Chasing");
                ChasePlayer();
                CheckAttackRange();
                break;

            case ZombieState.Attacking:
                Debug.Log("Zombie is Attacking");
                CheckAttackRange();
                break;
        }
    }

    private void Wander()
    {
        // Logic for wandering (move to wanderTarget)
        // If reached target, set a new target
        if (Vector3.Distance(transform.position, wanderTarget) < 1f)
        {
            SetWanderTarget();
        }

        // Calculate the direction to the wander target
        Vector3 direction = (wanderTarget - transform.position).normalized;

        // Calculate the rotation to look at the wander target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Rotate towards the target direction smoothly
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        // Move towards wander target
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // Set Animator parameters for blend tree
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveZ", direction.z);

        animator.SetBool("isWandering", true);
    }


    private void SetWanderTarget()
    {
        wanderTarget = Random.insideUnitSphere * wanderRadius + transform.position;
        wanderTarget.y = transform.position.y; // Keep the y level the same
    }

    private void CheckPlayerInFOV()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < FOV / 2 && Vector3.Distance(transform.position, player.position) < detectionDistance)
        {
            playerInFOV = true;
            playerLostTimer = 0;
            animator.SetBool("isWandering", false);
            animator.SetTrigger("playerDetected");

            // Change state to Detecting
            currentState = ZombieState.Detecting;

            // Start coroutine to transition to Chasing after Detect animation
            StartCoroutine(TransitionToChase());
        }
        else
        {
            playerLostTimer += Time.deltaTime;
            if (playerLostTimer > playerLostTime)
            {
                playerInFOV = false;
                currentState = ZombieState.Wandering; // Go back to wandering
                animator.SetBool("isChasing", false);
                animator.SetBool("isWandering", true);
            }
        }
    }

    private IEnumerator TransitionToChase()
    {
        // Wait until current animation has finished (adjust this duration as needed)
        yield return new WaitForSeconds(1.75f);

        // Now switch to Chasing
        currentState = ZombieState.Chasing;
        animator.SetBool("isChasing", true);
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // Continue chasing the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

            RotateTowardsPlayer();

            // Keep the Chasing animation active
            animator.SetBool("isChasing", true);
        }
        else
        {
            RotateTowardsPlayer();
            // Stop the Chasing animation and begin attacking
            animator.SetBool("isChasing", false);
            //currentState = ZombieState.Attacking;
            //PerformAttack(); don't need to be called here since CheckAttackRange() is in Update and it calls the PerformAttack()
        }
    }


    private void CheckAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Debug.Log("Player within range of zombie attack");
                lastAttackTime = Time.time;
                animator.SetBool("isAttacking", true);
                PerformAttack();
            }
        }
        else
        {
            // Player is out of range, stop attacking
            animator.SetBool("isAttacking", false);
            StartCoroutine(TransitionToChase());
        }
    }

    private void PerformAttack()
    {
        // Ensure the zombie is facing the player before attacking
        RotateTowardsPlayer();

        //switch between attacks
        switch (nextAttack)
        {
            case AttackType.RightHand:
                animator.SetTrigger("Attack_RightHand");
                nextAttack = AttackType.LeftHand;
                break;

            case AttackType.LeftHand:
                animator.SetTrigger("Attack_LeftHand");
                nextAttack = AttackType.RightHand_2;
                break;

            case AttackType.RightHand_2:
                animator.SetTrigger("Attack_RightHand_2");
                nextAttack = AttackType.LeftHand_2;
                break;

            case AttackType.LeftHand_2:
                animator.SetTrigger("Attack_LeftHand_2");
                nextAttack = AttackType.RightHand; // Reset to the first attack
                break;
        }
        StartCoroutine(TransitionToChaseAfterAttack());
    }

    private void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        }
    }

    private void StopAttacking()
    {
        // Reset the attack type and state
        nextAttack = AttackType.RightHand; // Reset to the first attack
        animator.SetFloat("AttackType", 0); // Reset the attack type
        currentState = ZombieState.Chasing; // Switch back to chasing state
    }

    private IEnumerator TransitionToChaseAfterAttack()
    {
        yield return new WaitForSeconds(1f); // Adjust based on the length of your attack animation
        currentState = ZombieState.Chasing;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Logic for death (disable components, play death animation, etc.)
        animator.SetTrigger("Die");
        this.enabled = false; // Disable this script
    }
}
