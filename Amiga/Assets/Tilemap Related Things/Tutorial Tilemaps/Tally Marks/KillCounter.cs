using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KillCounter : MonoBehaviour
{

    [SerializeField] private List<TileBase> tallyMarks;
    private Tilemap killCountTilemap;

    private int count = 0; // the number of enemies the player has defeated
    private int maxCount = 114 * 10; // the kill count wall is 114 squares large, each of which can hold 10

    private void Start ()
    {
        killCountTilemap = GetComponent<Tilemap> ();
    }

    public int getCount ()
    {
        return count;
    }

    // returns false if the kill count has reached its maximum
    public bool increaseCount ()
    {
        if (count < maxCount)
        {
            ++count;
            return true;
        }
        return false;
    }

    public void resetCount ()
    {
        count = 0;
    }

    // sets tiles so that the tally marks appear on the kill count wall
    public void createTallyMarks ()
    {
        int c = count;
        for (int row = 0; row < 11 && c > 0; ++row)
        {
            for (int col = 0; col < 15 - row && c > 0; ++col)
            {
                killCountTilemap.SetTile (new Vector3Int (-193 + col, 60 + row, 0), tallyMarks[c > 10 ? 9 : c - 1]);
                c -= c > 10 ? 10 : c;
            }
        }
        for (int col = 0; col < 3 && c > 0; ++col)
        {
            killCountTilemap.SetTile (new Vector3Int (-192 + col, 71, 0), tallyMarks[c > 10 ? 9 : c - 1]);
            c -= c > 10 ? 10 : c;
        }
        if (c > 0)
            killCountTilemap.SetTile (new Vector3Int (-191, 72, 0), tallyMarks[c > 10 ? 9 : c - 1]);
    }
}
