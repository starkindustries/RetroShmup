using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public GameObject explosionEffect;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet Hit: " + other.name);
        Damageable objectHit = other.GetComponent<Damageable>();
        if(objectHit != null)
        {
            objectHit.Damage();
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
