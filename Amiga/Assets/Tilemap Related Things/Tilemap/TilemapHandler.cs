// Belongs to the Destructible Tilemap

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHandler : MonoBehaviour
{

    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private List<Sprite> spriteMasks;

    private Tilemap destructibleTilemap;

    void Start()
    {
        destructibleTilemap = GetComponent<Tilemap> ();
    }

    // Destroys a tile at a location and replaces it with debris
    public void DestroyTile (Vector2 impactPos, Vector2 impactDir)
    {
        Vector3Int tilePos = destructibleTilemap.WorldToCell (impactPos);
        TileBase tileBase = destructibleTilemap.GetTile (tilePos);

        if (tileBase != null)
        {
            destructibleTilemap.SetTile (tilePos, null);
            Vector3 debrisPos = tilePos + new Vector3 (0.5f, 0.5f, 0);

            // create some pieces of debris
            for (int i = 0; i < spriteMasks.Count; ++i)
            {
                // instantiate one debris & grab its components
                GameObject debris = Instantiate (debrisPrefab, debrisPos, Quaternion.identity);
                Rigidbody2D rb = debris.GetComponent<Rigidbody2D> ();
                PolygonCollider2D collider = debris.GetComponent<PolygonCollider2D> ();
                SpriteRenderer sr = debris.GetComponent<SpriteRenderer> ();

                // update its collider
                collider.pathCount = spriteMasks[i].GetPhysicsShapeCount ();
                List<Vector2> path = new List<Vector2> ();
                for (int j = 0; j < collider.pathCount; ++j)
                {
                    path.Clear ();
                    spriteMasks[i].GetPhysicsShape (j, path);
                    collider.SetPath (j, path.ToArray ());
                }

                // update its sprite
                TileData tileData = new TileData ();
                tileBase.GetTileData (tilePos, destructibleTilemap, ref tileData);
                sr.sprite = tileData.sprite;
                debris.GetComponent<SpriteMask> ().sprite = spriteMasks[i];

                // adjust its size & add the force of impact
                debris.transform.localScale *= 0.95f;
                rb.AddForceAtPosition (impactDir, impactPos, ForceMode2D.Impulse);

            }

        }

    }

}
