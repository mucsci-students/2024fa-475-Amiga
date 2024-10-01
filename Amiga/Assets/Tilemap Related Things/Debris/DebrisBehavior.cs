// Belongs to the Debris prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehavior : MonoBehaviour
{

    private float destroyDistance = 20f;
    private float destroyTime;
    private float lifetime = 1.5f; // in seconds

    // Start is called before the first frame update
    void Start()
    {
        destroyTime = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToCamera = Vector2.Distance (transform.position, Camera.main.transform.position);
        if (distanceToCamera > destroyDistance)
            Destroy (gameObject);
        else if (Time.time > destroyTime)
            Destroy (gameObject);
    }

}
