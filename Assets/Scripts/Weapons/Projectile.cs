using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet Hit: " + other.name);

        // Check if the object is Damageable
        Damageable objectHit = other.GetComponent<Damageable>();
        if (objectHit == null)
        {
            return;
        }
        objectHit.Damage();
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
