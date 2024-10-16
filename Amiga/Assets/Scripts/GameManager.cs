using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reference to text display UI
    /// </summary>
    public GameObject textDisplay;

    void Update()
    {
        // Example: Trigger enemy spawn when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameEvents.TriggerEnemySpawn();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisplayText("Testing...");
        }
    }

    public void DisplayText(string text)
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }
}
