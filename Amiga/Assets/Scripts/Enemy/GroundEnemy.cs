using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : Enemy
{
    /// <summary> The direction of the enemy. 1/-1 for towards positive/negative x-axis. </summary>
    public int direction;

    /// <summary>
    /// Move action of the enemy.
    /// </summary>
    public virtual void Move()
    {
        transform.position = (Vector2)transform.position + new Vector2(speed * Time.deltaTime * direction, 0);
    }
}
