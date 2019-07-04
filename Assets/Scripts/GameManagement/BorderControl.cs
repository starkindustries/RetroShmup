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
        // Ignore trigger if it was the player
        // This is necessary because currently the player falls downwards and hits the boundary on death
        // The boundary destroys the player object before it can call GameOver()
        if (collision.tag == "Player")
        {
            return;
        }
        Destroy(collision.gameObject);
    }    
}
