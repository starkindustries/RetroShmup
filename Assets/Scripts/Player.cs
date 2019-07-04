using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    public int health;
    public float speed;    
    public GameObject explosionRing;
    public GameObject explosionNuclear;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private Vector2 screenBounds;
    private Weapon weapon;
    private float objectWidth;
    private float objectHeight;
    private bool isDead = false;
    private bool isDying = false; // so that we don't call death animation twice

    // Accelerometer variables
    private Quaternion initialTilt;    

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

        // Initialize weapon
        weapon = GetComponent<Weapon>();
        if (weapon != null)
        {
            weapon.ContinuousFire();
        }                

        if (GyroController.Instance.IsEnabled())
        {
            initialTilt = GyroController.Instance.GetAttitude().Value;
        }
    }

    // Note: As a general rule:
    // Input should be in Update(), so that there is no chance of having a frame 
    // in which you miss the player input, which could happen if you placed it in 
    // FixedUpdate().
    //         
    // Physics calculations should be in FixedUpdate(), so that they are consistent 
    // and synchronised with the global physics timestep of the game (by default 50 
    // times per second).
    // https://answers.unity.com/questions/620981/input-and-applying-physics-update-or-fixedupdate.html
    // Update is called once per frame
    private void Update()
    {
        if (gameManager.GameIsPaused())
        {
            return;
        }
        
        if(isDead)
        {
            return;
        }
            
        // Get Player Touch input
        /*
        foreach (Touch touch in Input.touches)
        {
            MoveByTouch(touch);
        }*/

        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            MoveByAxis(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        } 
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (GyroController.Instance.IsEnabled())
            {
                MoveByGyro(GyroController.Instance.GetAttitude().Value);
            }
        }        
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {
            // code
        }        
    }

    void MoveByAxis(float xAxis, float yAxis)
    {        
        Vector2 direction = new Vector2(xAxis, yAxis);

        rb.velocity = direction * speed;        

        // Keep player within screen boundaries
        // https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + objectWidth,  screenBounds.x - objectWidth);
        clampPosition.y = Mathf.Clamp(transform.position.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampPosition;
    }

    void MoveByTouch(Touch touch)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(touch.position);
        newPosition.z = transform.position.z;
        newPosition.x = newPosition.x + objectWidth * 2;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    void MoveByGyro(Quaternion orientation)
    {
        transform.Translate(x: orientation.x - initialTilt.x, orientation.y - initialTilt.y, 0);

        // Keep player within screen boundaries
        // https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        clampPosition.y = Mathf.Clamp(transform.position.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampPosition;
    }

    /*
    void MoveByAccelerometer(Vector3 acceleration)
    {
        // Accelerometer Input        
        // Vector2 direction = new Vector2(acceleration.x - initialTilt.x, acceleration.y - initialTilt.y);
        // rb.velocity = direction * speed;
        transform.Translate(x: acceleration.x - initialTilt.x, acceleration.y - initialTilt.y, 0);

        // Keep player within screen boundaries
        // https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
        Vector3 clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + objectWidth,  screenBounds.x - objectWidth);
        clampPosition.y = Mathf.Clamp(transform.position.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampPosition;
    }*/

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

        // Disable player's gravity
        rb.gravityScale = 0f;

        // Disable player sprite
        GetComponent<SpriteRenderer>().enabled = false;        
        
        // Release the nuclear explosion
        Vector2 explodeLocation = new Vector2(transform.position.x, screenBounds.y * -1);
        Instantiate(explosionNuclear, explodeLocation, transform.rotation);
        yield return new WaitForSeconds(0.75f);

        // Call game over
        Debug.Log("GAMEOVER checkpoint");
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
