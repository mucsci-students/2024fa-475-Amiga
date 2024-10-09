using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Define a custom event for enemy spawn
    public static event Action OnEnemySpawn;

    // Method to trigger the enemy spawn event
    public static void TriggerEnemySpawn()
    {
        OnEnemySpawn?.Invoke();
    }
}
