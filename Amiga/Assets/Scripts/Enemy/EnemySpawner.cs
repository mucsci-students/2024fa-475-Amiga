using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Reference to the random attachment prefab generator
    /// </summary>
    public GameObject attachmentGenerator;

    /// <summary>
    /// Reference to the enemy prefab
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// Radius for proximity check
    /// </summary>
    public float spawnRadius = 5.0f;

    /// <summary>
    /// Flag to ensure spawning only once per proximity
    /// </summary>
    private bool hasSpawned = false;

    void Update()
    {
        // Calculate the distance between the player and the spawner
        float distance = Vector2.Distance(player.transform.position, transform.position);

        // Check if the player is within the spawn radius and hasn't already triggered a spawn
        if (distance <= spawnRadius && !hasSpawned)
        {
            SpawnEnemy();
            hasSpawned = true; // Ensure only one spawn per proximity event
        }
        // Reset if the player moves away
        else if (distance > spawnRadius)
        {
            hasSpawned = false; // Reset the spawn trigger if the player moves away
        }
    }

    /// <summary>
    /// Spawn the enemy
    /// </summary>
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().player = player;
        enemy.GetComponent<Enemy>().attachmentGenerator = attachmentGenerator;
    }
}
