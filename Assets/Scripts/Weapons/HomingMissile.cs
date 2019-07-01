using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to Brackeys: How to make a Homing Missile in Unity
// https://youtu.be/0v_H3oOR0aU

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;

    private Transform target;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }    

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.velocity = transform.right * speed;
    }
}
