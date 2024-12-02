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

        GameObject newWeapon = Instantiate(data.equipPrefab, PlayerManager.instance.weaponHolder);
        newWeapon.GetComponent<Weapon>().weaponData = data;
        newWeapon.SetActive(false);

        weapons.Add(newWeapon);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index > 2) return;
        
        if (index >= weapons.Count) return;

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
        weapons.Remove(currentWeapon);
        Destroy(currentWeapon);
        currentWeapon = null;


        //If there is no space, swap the weapon with the current weapon
    }
}
