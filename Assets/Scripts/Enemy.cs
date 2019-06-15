using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, Damageable
{
    public int health;

    private Rigidbody2D rb;

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    void Die()
    {
        // make explosion
        rb.gravityScale = 1f;
    }

    #region Damageable Interface
    public int Health { get; set; }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            Die();
        }
    }
    #endregion
}
