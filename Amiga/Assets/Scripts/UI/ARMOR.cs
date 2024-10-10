using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMOR : MonoBehaviour
{
    /// <summary>
    /// The current hp of the player
    /// </summary>
    public float currentARMOR = 0.0f;

    /// <summary>
    /// The maximum hp of the player
    /// </summary>
    public float maxARMOR = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the current HP ratio (value between 0 and 1)
        float armorRatio = Mathf.Clamp(currentARMOR / maxARMOR, 0.0f, 1.0f);

        transform.localScale = new Vector3(armorRatio, transform.localScale.y, transform.localScale.z);
    }

    public void UpdateARMOR(float currentARMOR, float maxARMOR)
    {
        this.currentARMOR = currentARMOR;
        this.maxARMOR = maxARMOR;
    }
}
