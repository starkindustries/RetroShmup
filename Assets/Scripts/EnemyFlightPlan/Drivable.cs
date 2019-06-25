using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Drivable
{
    void Initialize(Vector2 position, Quaternion rotation);
    void SetVelocity(float velocity);
    void SetAngularVelocity(float angularVelocity);
    void Shoot(bool repeat, float delayBetweenShots);
}