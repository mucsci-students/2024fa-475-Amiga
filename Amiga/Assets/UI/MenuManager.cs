using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private List<GameObject> inventorySlots;
    private List<GameObject> staffSlots;

    [SerializeField] private GameObject staff;
    //[SerializeField] private GameObject inventorySlot;
    //[SerializeField] private GameObject staffSlot;

    // Start is called before the first frame update
    void Start()
    {
        /*
        for (float i = 0f; i < 100f; i += 20f)
        {
            for (float j = 0f; j < 20f; j += 20f)
            {
                GameObject iSlot = GameObject.Instantiate (inventorySlot);
                iSlot.transform.position = new Vector3 (i, j, 0f);
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
