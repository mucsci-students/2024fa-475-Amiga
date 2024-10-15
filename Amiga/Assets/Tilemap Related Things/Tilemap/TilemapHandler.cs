// Belongs to the Destructible Tilemap

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Audio;

public class TilemapHandler : MonoBehaviour
{

    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private List<Sprite> spriteMasks;
    [SerializeField] private Tilemap foregroundTilemap;
    [SerializeField] private Tilemap backgroundTilemap;
    [SerializeField] private List<AudioClip> crumbleSounds;

    private AudioSource src;
    private Tilemap destructibleTilemap;
    public int currentDebrisLayer = 1; // each debris should get its own layer
    public int maxDebrisLayer = 100; // 1 <= currentDebrisLayer <= 100

    void Start()
    {
        destructibleTilemap = GetComponent<Tilemap> ();
        src = GetComponent<AudioSource> ();
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

            DestroyRelatedTiles (tilePos);

            // create some pieces of debris
            for (int i = 0; i < spriteMasks.Count; ++i)
            {
                // instantiate one debris & grab its components
                GameObject debris = Instantiate (debrisPrefab, debrisPos, Quaternion.identity);
                Rigidbody2D rb = debris.GetComponent<Rigidbody2D> ();
                PolygonCollider2D collider = debris.GetComponent<PolygonCollider2D> ();
                SpriteRenderer rend = debris.GetComponent<SpriteRenderer> ();
                SpriteMask mask = debris.GetComponent<SpriteMask> ();

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
                rend.sprite = tileData.sprite;
                mask.sprite = spriteMasks[i];

                // give it a unique order in the sorting order
                rend.sortingOrder = currentDebrisLayer;
                
                mask.frontSortingOrder = currentDebrisLayer;
                mask.backSortingOrder = currentDebrisLayer - 1;
                if (++currentDebrisLayer > maxDebrisLayer) currentDebrisLayer = 1;

                // adjust its size & add the force of impact
                debris.transform.localScale *= 0.9f;
                rb.AddForceAtPosition (impactDir, impactPos, ForceMode2D.Impulse);

                // play a crumbling sound effect
                src.clip = crumbleSounds[Random.Range (0, crumbleSounds.Count)];
                src.Play ();

            }

        }

    }

    private void DestroyRelatedTiles (Vector3Int position)
    {
        if(foregroundTilemap != null && backgroundTilemap != null)
        {
            // destroy above
            foregroundTilemap.SetTile (position + new Vector3Int (0, 1, 0), null);
            backgroundTilemap.SetTile (position + new Vector3Int (0, 1, 0), null);
            // destroy below
            foregroundTilemap.SetTile (position + new Vector3Int (0, -1, 0), null);
            backgroundTilemap.SetTile (position + new Vector3Int (0, -1, 0), null);
            // destroy the right
            foregroundTilemap.SetTile (position + new Vector3Int (1, 0, 0), null);
            backgroundTilemap.SetTile (position + new Vector3Int (1, 0, 0), null);
            // destroy the left
            foregroundTilemap.SetTile (position + new Vector3Int (-1, 0, 0), null);
            backgroundTilemap.SetTile (position + new Vector3Int (-1, 0, 0), null);
        }
    }

}
