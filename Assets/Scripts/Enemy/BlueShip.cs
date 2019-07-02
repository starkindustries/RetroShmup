using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BlueShip : MonoBehaviour, Drivable
{
    private Rigidbody2D rb;
    private Weapon weapon;

    // Awake is used to initialize any variables or game state before the game starts.
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    public void Initialize(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public bool IsDrivable()
    {
        return true;
    }

    public void SetVelocity(float velocity)
    {
        rb.velocity = transform.up * velocity;
    }

    public void SetAngularVelocity(float angularVelocity)
    {
        rb.angularVelocity = angularVelocity;
    }

    public void Shoot(bool continuous, float fireRate)
    {
        Debug.Log("Shoot!");
        // Check if weapon is null
        if (weapon == null)
        {
            return;
        }

        // If continuous is true, begin continuous fire
        if (continuous)
        {
            weapon.fireRate = fireRate;
            weapon.ContinuousFire();
        } 
        else
        {
            weapon.SingleShot();
        }
    }

    public void AccelerateForward(float timeInSeconds)
    {
        Debug.LogWarning("BlueShip AccelerateForward: Implement this function");
    }
}
