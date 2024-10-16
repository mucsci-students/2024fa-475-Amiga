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

    /// <summary>
    /// Copy of the orginal x position
    /// </summary>
    public float xPos;

    private void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current HP ratio (value between 0 and 1)
        float armorRatio = Mathf.Clamp(currentARMOR / maxARMOR, 0.0f, 1.0f);

        transform.position = new Vector3(xPos - (1.0f - armorRatio) * 215.0f, transform.position.y, transform.position.z);
    }

    public void UpdateARMOR(float currentARMOR, float maxARMOR)
    {
        this.currentARMOR = currentARMOR;
        this.maxARMOR = maxARMOR;
    }
}
