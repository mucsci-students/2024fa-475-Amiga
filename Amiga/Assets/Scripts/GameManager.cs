using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Example: Trigger enemy spawn when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameEvents.TriggerEnemySpawn();
        }
    }
}
