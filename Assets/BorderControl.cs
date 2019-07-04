using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BorderControl : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("BorderControl reporting for duty!");
    }

    // Destroy GameObjects that leave the border/boundary
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("BorderControl detected: " + collision.name);
        Destroy(collision.gameObject);
    }    
}
