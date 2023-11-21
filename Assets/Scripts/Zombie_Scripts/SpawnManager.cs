using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public float spawnRadius = 10f;
    public int maxZombies = 10;

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Check if the maximum number of zombies has been reached
            if (GameObject.FindGameObjectsWithTag("Zombie").Length < maxZombies)
            {
                // Get a random point within the spawn radius
                Vector3 randomSpawnPoint = GetRandomSpawnPoint();

                // Choose a random zombie prefab from the array
                GameObject randomZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

                // Instantiate the chosen zombie prefab at the random spawn point
                Instantiate(randomZombiePrefab, randomSpawnPoint, Quaternion.identity);
            }

            // Wait for the next spawn interval
            yield return new WaitForSeconds(5f);
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        // Calculate a random point within the spawn radius
        float x = transform.position.x + Mathf.Cos(randomAngle) * spawnRadius;
        float z = transform.position.z + Mathf.Sin(randomAngle) * spawnRadius;

        // Return the random spawn point
        return new Vector3(x, transform.position.y, z);
    }
}
