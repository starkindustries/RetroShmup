using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Weapon weapon;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        weapon = GetComponent<Weapon>();
        weapon.SetAnimator(animator);
        if (weapon != null)
        {
            weapon.ContinuousFire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
