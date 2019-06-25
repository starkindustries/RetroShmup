using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BlueShip : MonoBehaviour, Drivable
{
    private Rigidbody2D rb;

    // Awake is used to initialize any variables or game state before the game starts.
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float velocity)
    {
        if(rb == null)
        {
            Debug.LogError("ERROR: rigidbody is null!");
        }
        rb.velocity = transform.up * velocity;
    }

    public void SetAngularVelocity(float angularVelocity)
    {
        rb.angularVelocity = angularVelocity;
    }

    public void Shoot(bool repeat, float delayBetweenShots)
    {
        Debug.Log("Shoot!");
    }
}
