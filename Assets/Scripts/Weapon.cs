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

    // Start is called before the first frame update
    void Start()
    {
        if(burstMode)
        {
            StartCoroutine(BurstFire());
        }
        else
        {
            StartCoroutine(Fire());
        }        
    }
    
    IEnumerator BurstFire()
    {
        while(true)
        {
            // shoot for x seconds
            for(float time = 0; time < continuousFireTime; time += 1/ fireRate)
            {                
                if (aimAtPlayer)
                {
                    firePoint.right = GameManager.Instance.GetPlayerTransform().position - firePoint.position;
                }
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(1 / fireRate);
            }
            // cooldown for x seconds
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    IEnumerator Fire()
    {
        while(true)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(1 / fireRate);
        }                
    }
}
