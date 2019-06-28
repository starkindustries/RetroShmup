using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, Damageable, Scorable, Drivable
{
    public GameObject explosion;
    public GameObject pointsEffect;

    public int health;
    public int points;
    public float speed;
    public float rotationSpeed;    
    
    private bool isDead = false;
    private Rigidbody2D rb;
    private Weapon weapon;

    #region Awake, Start & Update
    private void Awake()
    {
        // Get the rigidbdy
        rb = GetComponent<Rigidbody2D>();
        
        // Get weapon if available
        weapon = GetComponent<Weapon>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Health = health;
        Points = points;
        Scoreboard = FindObjectOfType<Scoreboard>();        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // For Physics!
    private void FixedUpdate()
    {

    }
    #endregion

    private void Die()
    {
        isDead = true;        
        Score();
        rb.gravityScale = 1f;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
        yield break;
    }

    #region Damageable Interface
    // ************************
    // Damageable Interface
    // ************************
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
    // ************************
    // Scorable Interface
    // ************************
    public int Points { get; set; }
    public Scoreboard Scoreboard { get; set; }

    public void Score()
    {
        Scoreboard.AddPoints(Points);
        Instantiate(pointsEffect, transform.position, Quaternion.identity);
    }
    #endregion

    #region Drivable Interface
    // ************************
    // Drivable Interface
    // ************************
    public void Initialize(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public bool IsDrivable()
    {
        return !isDead;
    }

    public void SetVelocity(float velocity)
    {
        if (rb == null)
        {
            Debug.LogWarning("Warning! Rigidbody is null in Enemy");
            rb = GetComponent<Rigidbody2D>();
        }
        rb.velocity = transform.right * velocity;
    }

    public void SetAngularVelocity(float angularVelocity)
    {
        rb.angularVelocity = angularVelocity;
    }

    public void Shoot(bool continuous, float fireRate)
    {
        Debug.Log("Shoot!");
        // Check if weapon is null
        if (weapon == null)
        {
            return;
        }

        // If continuous is true, begin continuous fire
        if (continuous)
        {
            weapon.fireRate = fireRate;
            weapon.ContinuousFire();
        }
        else
        {
            weapon.SingleShot();
        }
    }
    #endregion
}
