using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SliderSFX : MonoBehaviour
{

    private float lastPos = 0f;
    private float distSinceLastSound = 0f;
    private float distUntilSound = 3f;
    private float timeOfLastSound = 0f;
    private float timeUntilSound = 0.1f;
    [SerializeField] private AudioSource src;

    public void Slide (float pos)
    {
        distSinceLastSound += Mathf.Abs (pos - lastPos);
        if (distSinceLastSound > distUntilSound && Time.unscaledTime > timeOfLastSound + timeUntilSound)
        {
            src.Play ();
            distSinceLastSound = 0;
            timeOfLastSound = Time.unscaledTime;
        }
        lastPos = pos;
    }
}
