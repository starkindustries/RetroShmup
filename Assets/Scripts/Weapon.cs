using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;  
    
    // Fire rate determines the number of rounds per second.
    public float fireRate;

    // Burst mode will allow a weapon to fire continuously and then cooldown
    // ContinuousFireTime is the length of time the weapon can fire without "overheating"
    // CooldownTime is the length of time to stop shooting and "cooldown"
    public bool burstMode; 
    public float continuousFireTime;
    public float cooldownTime;
    public bool aimAtPlayer;
    private bool ceaseFire;

    // Start is called before the first frame update
    void Start()
    {
        ceaseFire = false;
    }

    // Public Functions
    public void CeaseFire()
    {
        ceaseFire = true;
    }

    public void SingleShot()
    {        
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }

    public void SingleRoundBurst()
    {        
        StartCoroutine(SingleRoundBurstCoroutine());
    }

    public void ContinuousFire()
    {
        ceaseFire = false;
        StartCoroutine(ContinuousFireCoroutine());
    }

    public void ContinuousBurstFire()
    {
        ceaseFire = false;
        StartCoroutine(ContinuousBurstFireCoroutine());
    }

    // Private Coroutines
    private IEnumerator SingleRoundBurstCoroutine()
    {
        // shoot for x seconds
        for (float time = 0; time < continuousFireTime; time += 1 / fireRate)
        {
            if (aimAtPlayer)
            {
                firePoint.right = GameManager.Instance.GetPlayerTransform().position - firePoint.position;
            }
            SingleShot();
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

    private IEnumerator ContinuousBurstFireCoroutine()
    {
        while(!ceaseFire)
        {
            yield return StartCoroutine(SingleRoundBurstCoroutine());
            // cooldown for x seconds
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    private IEnumerator ContinuousFireCoroutine()
    {
        while(!ceaseFire)
        {
            SingleShot();
            yield return new WaitForSeconds(1 / fireRate);
        }                
    }
}
