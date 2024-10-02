using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Reference to the enemy prefab
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// # of seconds between enemy spawn
    /// </summary>
    public float spawnInterval;

    void Start()
    {
        spawnInterval = 1.0f;
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Spawn an enemy every spawn interval
    /// </summary>
    /// <returns> wait </returns>
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
