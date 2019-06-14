using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        // AudioManager.Instance.Play("Shoot");
    }
}
