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

    // Subscribe to the event
    void OnEnable()
    {
        GameEvents.OnEnemySpawn += SpawnEnemy;  // Subscribe to the custom event
    }

    // Unsubscribe from the event
    void OnDisable()
    {
        GameEvents.OnEnemySpawn -= SpawnEnemy;  // Unsubscribe to avoid memory leaks
    }

    // Spawn the enemy when the custom event is triggered
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().player = player;
        enemy.GetComponent<Enemy>().attachmentGenerator = attachmentGenerator;
    }
}
