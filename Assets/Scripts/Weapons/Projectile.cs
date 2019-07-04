using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {        
        Destroy(this.gameObject, 2.0f);
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
