using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public int totalAmmo, magAmmo;
    public float fireTimer;

    public Transform firingPoint; 

    void Start()
    {
        magAmmo = weaponData.magazineSize;
        totalAmmo = weaponData.capacity;
    }

    void Update()
    {
        if (weaponData.isAutomatic)
        {
            if (Input.GetMouseButton(0) && fireTimer <= 0)
            {
                Shoot();
                fireTimer = weaponData.fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime; 
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && fireTimer <= 0)
            {
                Shoot();
                fireTimer = weaponData.fireRate;
            }
            else{
                fireTimer -= Time.deltaTime; 
            }
        }
    }

    void Shoot()
    {
        float spreadAngle = Random.Range(-weaponData.spread, weaponData.spread);
        Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * firingPoint.up;

        GameObject bullet = Instantiate(weaponData.bulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<Bullet>().damage = weaponData.damage;
    }
}

