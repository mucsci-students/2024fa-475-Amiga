using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{

    [SerializeField] private Staff staff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag ("Player"))
        {
            staff.LevelUpStaff ();
            Destroy (gameObject);
        }
    }
}
