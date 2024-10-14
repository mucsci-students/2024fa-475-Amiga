using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueBehavior : MonoBehaviour
{
    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private List<Sprite> spriteMasks;
    [SerializeField] private TilemapHandler tilemapHandler; // Handler to get current debris layer, increment it
    private SpriteRenderer spriteRenderer;

    private void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DestroyStatue(Vector2 impactPos, Vector2 impactDir)
    {
        // Create some pieces of debris
        for (int i = 0; i < spriteMasks.Count; ++i)
        {
            // Instantiate one debris & grab its components
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
            PolygonCollider2D collider = debris.GetComponent<PolygonCollider2D>();
            SpriteRenderer rend = debris.GetComponent<SpriteRenderer>();
            SpriteMask mask = debris.GetComponent<SpriteMask>();

            // Update its collider
            collider.pathCount = spriteMasks[i].GetPhysicsShapeCount();
            List<Vector2> path = new List<Vector2>();
            for (int j = 0; j < collider.pathCount; ++j)
            {
                path.Clear();
                spriteMasks[i].GetPhysicsShape(j, path);
                collider.SetPath(j, path.ToArray());
            }

            // Update its sprite
            rend.sprite = spriteRenderer.sprite;
            rend.color = spriteRenderer.color;
            mask.sprite = spriteMasks[i];

            // Give it a unique order in the sorting order
            rend.sortingOrder = tilemapHandler.currentDebrisLayer;
            mask.frontSortingOrder = tilemapHandler.currentDebrisLayer;
            mask.backSortingOrder = tilemapHandler.currentDebrisLayer - 1;

            if (++tilemapHandler.currentDebrisLayer > tilemapHandler.maxDebrisLayer)
                tilemapHandler.currentDebrisLayer = 1;

            // Adjust its size & add the force of impact
            debris.transform.localScale *= 0.9f;
            rb.AddForceAtPosition(impactDir, impactPos, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
