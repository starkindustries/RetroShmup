using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

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
}
