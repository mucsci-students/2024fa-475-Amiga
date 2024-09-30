// Belongs to the DestroyTilesOnClick prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTilesOnClick : MonoBehaviour
{

    [SerializeField] private Tilemap destructibleTilemap;
    [SerializeField] private TilemapHandler handler;
    [SerializeField] private Camera cam;
    [SerializeField] private float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
        if (Input.GetMouseButton (0))
        {
            Vector3 debrisPos = destructibleTilemap.WorldToCell (mousePos) + new Vector3 (0.5f, 0.5f, 0f);
            handler.DestroyTile (mousePos, force * (debrisPos - mousePos));
        }
    }
}
