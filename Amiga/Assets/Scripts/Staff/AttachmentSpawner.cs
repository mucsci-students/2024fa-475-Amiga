using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSpawner : MonoBehaviour
{
    /// <summary>
    /// Reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Reference to the attachment prefab
    /// </summary>
    public GameObject attachmentPrefab;

    /// <summary>
    /// # of seconds between enemy spawn
    /// </summary>
    public float spawnInterval;

    void Start()
    {
        spawnInterval = 5.0f;
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
            Instantiate(attachmentPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
