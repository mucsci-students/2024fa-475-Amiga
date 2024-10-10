using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MANA : MonoBehaviour
{
    /// <summary>
    /// The current hp of the player
    /// </summary>
    public float currentMANA = 0.0f;

    /// <summary>
    /// The maximum hp of the player
    /// </summary>
    public float maxMANA = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the current HP ratio (value between 0 and 1)
        float manaRatio = Mathf.Clamp(currentMANA / maxMANA, 0.0f, 1.0f);

        transform.localScale = new Vector3(manaRatio, transform.localScale.y, transform.localScale.z);
    }

    public void UpdateMANA(float currentMANA, float maxMANA)
    {
        this.currentMANA = currentMANA;
        this.maxMANA = maxMANA;
    }
}
