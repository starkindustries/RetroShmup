using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    public int health;
    public float speed;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.GameIsPaused())
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // AudioManager.Instance.Play("Shoot");
    }

    void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(xAxis, yAxis);
        rb.velocity = movement * speed;
        // rb.AddForce(movement * speed);                
    }    

    void Die()
    {
        // Explode
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        Damageable objectHit = other.GetComponent<Damageable>();
        if (objectHit != null)
        {
            // Inflict damage on the object hit
            objectHit.Damage();
            // And inflict damage on self
            this.Damage();
        }
        // Instantiate(explosionEffect, transform.position, Quaternion.identity);        
    }
}
