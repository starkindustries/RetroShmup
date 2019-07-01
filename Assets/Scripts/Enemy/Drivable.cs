using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Drivable
{
    void Initialize(Vector2 position, Quaternion rotation);

    bool IsDrivable();

    void SetVelocity(float velocity);
    void SetAngularVelocity(float angularVelocity);
    void Shoot(bool continuous, float fireRate);    
}