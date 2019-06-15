using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public interface IDamageable
    {
        int Health { get; set; }
        void Damage();
    }
}
