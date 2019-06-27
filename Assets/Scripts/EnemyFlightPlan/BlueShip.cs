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

    public void Initialize(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetVelocity(float velocity)
    {
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
