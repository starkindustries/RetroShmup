using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    public int health;
    public float speed;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject explosionRing;
    public GameObject explosionNuclear;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private bool isDead = false;
    private bool isDying = false; // so that we don't call death animation twice

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log("Screen bounds: " + screenBounds.ToString());
        objectWidth  = GetComponent<SpriteRenderer>().bounds.extents.x; // extents = size of width / 2
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y; // extents = size of height / 2
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
        if(!isDead)
        {
            Move();
        }        
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

        // Keep player within screen boundaries
        // https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + objectWidth,  screenBounds.x - objectWidth);
        clampPosition.y = Mathf.Clamp(transform.position.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampPosition;
    }    

    void Die()
    {        
        if(!isDying)
        {
            StartCoroutine(Explode());
        }        
        isDead = true;
    }

    private IEnumerator Explode()
    {
        isDying = true;
        rb.gravityScale = 3.0f;
        while (transform.position.y > screenBounds.y * -1)
        {
            Instantiate(explosionRing, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        
        // Disable player sprite
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        
        // Release the nuclear explosion
        Vector2 explodeLocation = new Vector2(transform.position.x, screenBounds.y * -1);
        Instantiate(explosionNuclear, explodeLocation, transform.rotation);
        yield return new WaitForSeconds(0.75f);

        // Call game over
        GameManager.Instance.GameOver();
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
