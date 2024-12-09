using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public int totalAmmo, magAmmo;
    public float fireTimer;
    public bool isReloading;

    public Transform firingPoint; 

    void Start()
    {
        magAmmo = weaponData.magazineSize;
        totalAmmo = PlayerManager.instance.inventory.SelectAmmo(weaponData.bulletType);
    }

    void Update()
    {
        if (weaponData.isAutomatic)
        {
            if (Input.GetMouseButton(0) && fireTimer <= 0 && magAmmo > 0 && !isReloading)
            {
                Shoot();
                fireTimer = weaponData.fireRate;
                magAmmo--;
            }
            else
            {
                fireTimer -= Time.deltaTime; 
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && fireTimer <= 0 && magAmmo > 0 && !isReloading)
            {
                Shoot();
                fireTimer = weaponData.fireRate;
                magAmmo--;
            }
            else{
                fireTimer -= Time.deltaTime; 
            }
        }

        if (Input.GetMouseButtonDown(0) && magAmmo <= 0 && !isReloading && totalAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && magAmmo < weaponData.magazineSize && totalAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (weaponData.bulletCount > 1)
        {
            float spreadAngle = -weaponData.spread;

            for (int i = 0; i < weaponData.bulletCount; i++)
            {
                Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * firingPoint.up;

                GameObject bullet = Instantiate(weaponData.bulletPrefab, firingPoint.position, Quaternion.LookRotation(Vector3.forward, spreadDirection));

                spreadAngle += weaponData.spread / weaponData.bulletCount * 2;

                bullet.GetComponent<Bullet>().damage = weaponData.damage;
            }
        }
        else
        {
            float spreadAngle = Random.Range(-weaponData.spread, weaponData.spread);
            Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * firingPoint.up;

            GameObject bullet = Instantiate(weaponData.bulletPrefab, firingPoint.position, Quaternion.LookRotation(Vector3.forward, spreadDirection));
            bullet.GetComponent<Bullet>().damage = weaponData.damage;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reload Started");
        
        // Wait for reload time
        yield return new WaitForSeconds(weaponData.reloadTime);

        int ammoNeeded = weaponData.magazineSize - magAmmo;

        if (totalAmmo >= ammoNeeded)
        {
            magAmmo += ammoNeeded;
            totalAmmo -= ammoNeeded;
        }
        else
        {
            magAmmo += totalAmmo;
            totalAmmo = 0;
        }

        isReloading = false;
        Debug.Log("Reload Ended");
    }
}

