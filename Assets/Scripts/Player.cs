using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
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
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
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
}
