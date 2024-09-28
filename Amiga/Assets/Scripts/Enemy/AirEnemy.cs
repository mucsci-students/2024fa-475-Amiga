using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AirEnemy : Enemy
{
    /// <summary> The normalized direction of the enemy. </summary>
    public Vector2 direction;

    /// <summary>
    /// Fly action of the enemy.
    /// </summary>
    public virtual void Fly()
    {
        transform.position = (Vector2)transform.position + (speed * Time.deltaTime * direction);
    }
}
