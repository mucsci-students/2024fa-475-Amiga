using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /// <summary> The health of the enemy. </summary>
    private int m_health;

    /// <summary> The speed of the enemy. </summary>
    private float m_speed;

    /// <summary> The # of seconds between each attack of the enemy. </summary>
    private float m_attackInterval;

    /// <summary> The transform of the enemy. </summary>
    private Transform m_transform;

    /// <summary>
    /// Attack action of the enemy.
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// Take given amount of damage.
    /// </summary>
    /// <param name="damage"> The amount of damage taken. </param>
    public abstract void TakeDamage(int damage);

    /// <summary>
    /// Die action of the enemy.
    /// </summary>
    public abstract void Die();
}
