using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, Damageable, Scorable
{
    public GameObject explosion;
    public GameObject pointsEffect;

    public int health;
    public int points;
    public float speed;
    public float rotationSpeed;    
    
    private bool isDead = false;
    private Rigidbody2D rb;
    private int currentTargetIndex;
    private Vector2[] flightPath;
    private float minDistance = 1f;

    #region Start & Update
    // Start is called before the first frame update
    private void Start()
    {
        Health = health;
        Points = points;
        Scoreboard = FindObjectOfType<Scoreboard>();
        rb = GetComponent<Rigidbody2D>();

        // Set starting position
        transform.position = flightPath[0];
        
        currentTargetIndex = 1;

        // Flip the enemy
        transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // For Physics!
    private void FixedUpdate()
    {
        if(!isDead)
        {
            TraverseFlightPath();
        }
    }
    #endregion

    public void SetFlightPath(Vector2[] path)
    {
        flightPath = path;
    }

    public void TraverseFlightPath()
    {
        float distance = Vector2.Distance(flightPath[currentTargetIndex], rb.position);
        if ((distance < minDistance))
        {            
            if (currentTargetIndex < flightPath.Length - 1)
            {
                // if current index is less than the last index then increment
                currentTargetIndex++;
            }
            else
            {
                // else if the current index is on the last index then journey complete!
                Destroy(this.gameObject);
            }
        }

        Vector2 direction = flightPath[currentTargetIndex] - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed; // float        
        rb.velocity = transform.right * speed; // vector2
    }

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
}
