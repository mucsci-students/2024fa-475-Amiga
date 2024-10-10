using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    /// <summary>
    /// The current hp of the player
    /// </summary>
    public float currentHP = 0.0f;

    /// <summary>
    /// The maximum hp of the player
    /// </summary>
    public float maxHP = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the current HP ratio (value between 0 and 1)
        float hpRatio = Mathf.Clamp(currentHP / maxHP, 0.0f, 1.0f);

        transform.localScale = new Vector3(hpRatio, transform.localScale.y, transform.localScale.z);
    }

    public void UpdateHP(float currentHP, float maxHP)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
    }
}
