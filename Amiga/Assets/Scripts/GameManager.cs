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

    [SerializeField]
    public string[] lavaText =
    {
        "\"He\" felt hot...",
        "\"He doesn't like it...\"",
        "Really hot...?",
        "Naught boy..."
    };

    //LEVEL META DATA-----------------------------------------

    /// <summary>
    /// Number current level.
    /// </summary>
    public int level;

    /// <summary>
    /// Number of enemy killed.
    /// </summary>
    public int enemyKilled;

    private void Start()
    {
        textDisplay = GameObject.Find("Text Display");

        level = 0;
        enemyKilled = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisplayText("Testing...");
        }
    }

    public void NextLevel()
    {
        ++level;
    }

    public void NextKill()
    {
        ++enemyKilled;
    }

    public void Reset()
    {
        level = 0;
        enemyKilled = 0;
    }

    public void DisplayText(string text)
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }

    public string RandomText(string[] candidate)
    {
        int index = Random.Range(0, candidate.Length);
        return candidate[index];
    }

    /// <summary>
    /// Display text related to lava
    /// </summary>
    public void DisplayLavaText()
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(RandomText(lavaText));
    }

    /// <summary>
    /// Display text related to player being hurt
    /// </summary>
    public void DisplayHurtText()
    {
        string text = "Is it painful...";
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }

    /// <summary>
    /// Display text related to player death
    /// </summary>
    public void DisplayDeathText()
    {
        //string text = "Poor man...";
        string text = "This one looks stronger...";
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }

    /// <summary>
    /// Display text related to enemy killed
    /// </summary>
    public void DisplayKillText()
    {
        string text = "That's it...";
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }

    /// <summary>
    /// Display text related to attachment collected
    /// </summary>
    public void DisplayCollectAttachmentText()
    {
        string text = "more...";
        textDisplay.GetComponent<TextDisplay>().TriggerText(text);
    }
}
