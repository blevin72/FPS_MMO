using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;          // Zombie's movement speed
    public float changeDirectionInterval = 2.0f;  // Time interval to change direction
    private float nextDirectionChangeTime;  // Time for the next direction change
    private Vector3 randomDirection;       // Random direction for movement

    void Start()
    {
        // Initialize the first direction change time
        nextDirectionChangeTime = Time.time + Random.Range(0, changeDirectionInterval);
    }

    void Update()
    {
        // Check if it's time to change direction
        if (Time.time >= nextDirectionChangeTime)
        {
            // Generate a random direction for movement
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            // Normalize the direction vector and set the next direction change time
            randomDirection.Normalize();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Move the zombie in the random direction
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }
}






