using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, Activatable
{
    private Weapon weapon;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();        
    }

    public void Activate()
    {
        weapon.SetAnimator(animator);
        if (weapon != null)
        {
            weapon.ContinuousFire();
        }
    }
}
