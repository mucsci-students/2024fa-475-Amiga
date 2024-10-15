using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WaterTileSFX : MonoBehaviour
{

    private AudioSource src;
    [SerializeField] private List<AudioClip> splashSounds;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        src.PlayOneShot (splashSounds[Random.Range (0, splashSounds.Count)]);
    }
}
