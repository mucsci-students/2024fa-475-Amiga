using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] private GameObject inventorySlotPrefab;
    public List<GameObject> inventorySlots;

    void Start()
    {
        int i = 0;
        for (float x = -225f - 9f; x <= 225 + 9f; x += 50f + 2f)
        {
            for (float y = -152f - 2f; y <= -52f + 2f; y += 50f + 2f)
            {
                inventorySlots.Add (GameObject.Instantiate (inventorySlotPrefab, canvas.transform, false));
                inventorySlots[i].transform.localPosition = new Vector3 (x, y, 0f);
                ++i;
            }
        }
    }

}
