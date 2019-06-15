using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, Damageable, Scorable
{
    public int health;
    public int points;

    private Rigidbody2D rb;

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        Points = points;
        Scoreboard = FindObjectOfType<Scoreboard>();
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
        Score();
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

    #region Scorable Interface
    public int Points { get; set; }
    public Scoreboard Scoreboard { get; set; }

    public void Score()
    {
        Scoreboard.AddPoints(Points);
    }
    #endregion
}
