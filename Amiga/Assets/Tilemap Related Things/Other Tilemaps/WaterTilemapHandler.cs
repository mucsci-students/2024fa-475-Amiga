using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTilemapHandler : MonoBehaviour
{

    private float buoyantForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D (Collider2D collider)
    {
        print ("ahh");
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D> ();
        if (rb != null)
        {
            print ("player");
            if (rb.velocity.y < buoyantForce)
            {
                rb.AddForce (Vector2.up * buoyantForce);
            }
        }
    }
}
