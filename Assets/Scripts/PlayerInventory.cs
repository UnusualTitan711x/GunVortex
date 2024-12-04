using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }

    public void AddWeapon(WeaponData data) // Add field for current ammo later
    {
        if (currentWeapon != null && weapons.Count >= 2)
        {
            SwapWeapon(data);
            return;
        }

        GameObject newWeapon = Instantiate(data.equipPrefab, PlayerManager.instance.weaponHolder);
        newWeapon.GetComponent<Weapon>().weaponData = data;
        
        if (weapons.Count >= 1) newWeapon.SetActive(false);
        if (weapons.Count == 0) currentWeapon = newWeapon;

        weapons.Add(newWeapon);
        
        
    }

    public void EquipWeapon(int index)
    {
        //if (index < 0 || index > 2) return;
        
        if (index < 0 || index >= weapons.Count) return;

        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        currentWeapon = weapons[index];
        currentWeapon.SetActive(true);
    }

    public void DropWeapon() // Don't forget to put ammo too
    {
        if (currentWeapon == null) return;

        // Remove the current weapon from the inventory
        weapons.Remove(currentWeapon);

        // Spawn the weapon pickup in the world
        GameObject worldPickup = Instantiate(currentWeapon.GetComponent<Weapon>().weaponData.pickupPrefab, PlayerManager.instance.weaponHolder.position, PlayerManager.instance.weaponHolder.rotation, null);
        worldPickup.transform.SetParent(null);
        // worldPickup.GetComponent<WeaponPickup>().ammo = currentWeapon.GetComponent<Weapon>().magAmmo;
        Destroy(currentWeapon);
        currentWeapon = null;
    }

    public void SwapWeapon(WeaponData data)
    {
        DropWeapon();

        GameObject newWeapon = Instantiate(data.equipPrefab, PlayerManager.instance.weaponHolder);
        newWeapon.GetComponent<Weapon>().weaponData = data;
        weapons.Add(newWeapon);
        EquipWeapon(1);
    }
}
