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
        // Initialise the ammo in the magazine
        magAmmo = weaponData.magazineSize;

        // Get the ammo reserve from the inventory, depending on the ammo type
        totalAmmo = PlayerManager.instance.inventory.SelectAmmo(weaponData.bulletType);

        UI_Manager.instance.weaponNameText.SetText(weaponData.weaponName + ":");
        UI_Manager.instance.reloadingText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // This is to fix a bug where the player cannot shoot if you were reloading and you switched weapons
        isReloading = false;

        //UI_Manager.instance.weaponNameText.SetText(weaponData.weaponName);
    }

    void Update()
    {
        // Shoot indifferently for automatic and non-aitomatic guns
            
        // Check for input and other conditions before shooting
        if (PlayerManager.instance.player.GetComponent<Player>().rotationDirection.magnitude >= 0.8 && fireTimer <= 0 && magAmmo > 0 && !isReloading)
        {
            Shoot();   
        }
        else
        {
            fireTimer -= Time.deltaTime; 
        }

        // Check for some conditions before reloading

        // If shooting is attempted with an empty magazine, then reload
        if (PlayerManager.instance.player.GetComponent<Player>().rotationDirection.magnitude >= 0.8 && magAmmo <= 0 && !isReloading && totalAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        // If the R key is pressed and some other conditions are met, then reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }

        UI_Manager.instance.ammoText.SetText(magAmmo.ToString());
    }

    // Shoot function
    public void Shoot()
    {
        // Check for conditions before shooting
        if (fireTimer > 0.001 || magAmmo <= 0 || isReloading) return;


        // Decrease ammo and reset fire timer
        fireTimer = weaponData.fireRate;
        magAmmo--;

        // Check if the gun shoots multiple bullets at once
        if (weaponData.bulletCount > 1)
        {
            float spreadAngle = -weaponData.spread;

            // Repeat for every bullet that is there to shoot
            for (int i = 0; i < weaponData.bulletCount; i++)
            {   
                // calculating spread direction
                Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * firingPoint.up;

                // Spawn bullet and apply spread
                GameObject bullet = Instantiate(weaponData.bulletPrefab, firingPoint.position, Quaternion.LookRotation(Vector3.forward, spreadDirection));

                // Adjust the spread angle for the next bullet
                spreadAngle += weaponData.spread / weaponData.bulletCount * 2;

                // Set the damage caused by the bullet to be the same as that set in the weapon data
                bullet.GetComponent<Bullet>().damage = weaponData.damage;
            }
        }
        else // Basically the same thing but do not spawn multiple bullets
        {
            float spreadAngle = Random.Range(-weaponData.spread, weaponData.spread);
            Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * firingPoint.up;

            GameObject bullet = Instantiate(weaponData.bulletPrefab, firingPoint.position, Quaternion.LookRotation(Vector3.forward, spreadDirection));
            bullet.GetComponent<Bullet>().damage = weaponData.damage;
        }
    }

    public void ReloadWeapon()
    {
        if (isReloading || magAmmo >= weaponData.magazineSize || totalAmmo <= 0) return;

        StartCoroutine(Reload());
    }

    // Reload Coroutine
    IEnumerator Reload()
    {
        // Set reloading to true to indicate that you are still reloading
        isReloading = true;
        Debug.Log("Reload Started");
        UI_Manager.instance.reloadingText.gameObject.SetActive(true);
        
        // Wait for reload time
        yield return new WaitForSeconds(weaponData.reloadTime);

        // Calculate the ammo needed for the reload
        int ammoNeeded = weaponData.magazineSize - magAmmo;

        // Adjust the ammo in magazine and reserve depending on the situation
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

        // Stopped reloading
        isReloading = false;
        Debug.Log("Reload Ended");
        UI_Manager.instance.reloadingText.gameObject.SetActive(false);

        // Update the ammo in the inventory 
        PlayerManager.instance.inventory.UpdateAmmo(weaponData.bulletType, totalAmmo);
    }
}

