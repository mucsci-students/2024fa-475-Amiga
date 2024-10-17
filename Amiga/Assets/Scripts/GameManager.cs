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

    [SerializeField]
    public string[] hurtText =
    {
        "Is it really painful...?",
        "How does it feel...?",
        "Is \"he\" bleeding...?"
    };

    [SerializeField]
    public string[] deathText =
    {
        "This one looks stronger...",
        "Poor man...",
        "Need a stronger body..."
    };

    [SerializeField]
    public string[] killText =
    {
        "That's it...",
        "Kill..."
    };

    [SerializeField]
    public string[] collectAttachmentText =
    {
        "more...",
        "I love it..."
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
        textDisplay.GetComponent<TextDisplay>().TriggerText(RandomText(hurtText));
    }

    /// <summary>
    /// Display text related to player death
    /// </summary>
    public void DisplayDeathText()
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(RandomText(deathText));
    }

    /// <summary>
    /// Display text related to enemy killed
    /// </summary>
    public void DisplayKillText()
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(RandomText(killText));
    }

    /// <summary>
    /// Display text related to attachment collected
    /// </summary>
    public void DisplayCollectAttachmentText()
    {
        textDisplay.GetComponent<TextDisplay>().TriggerText(RandomText(collectAttachmentText));
    }
}
