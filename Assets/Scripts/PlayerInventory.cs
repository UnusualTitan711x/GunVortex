using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // List that contains the weapons
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;

    // Where the bullets are stored
    public int smallAmmo = 0;
    public int mediumAmmo = 0;
    public int shotgunShells = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // Switch weapons using the 1 and 2 keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }

        // Drop the weapon using the Q key
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }

    public void AddWeapon(WeaponData data) // Add field for current ammo later
    {
        // Swap weapon if the space for weapon is full
        if (currentWeapon != null && weapons.Count >= 2)
        {
            SwapWeapon(data);
            return;
        }

        // Put the weapon in the player's hands
        GameObject newWeapon = Instantiate(data.equipPrefab, PlayerManager.instance.weaponHolder);
        newWeapon.GetComponent<Weapon>().weaponData = data;
        
        // Add the weapon to the inventory if there is already a weapon in the inventory.
        // Else, the weapon added is equipped
        if (weapons.Count >= 1) newWeapon.SetActive(false);
        if (weapons.Count == 0) currentWeapon = newWeapon;

        weapons.Add(newWeapon);
    }

    public void EquipWeapon(int index)
    {
        // Avoid going out of bounds of the weapons list
        if (index < 0 || index >= weapons.Count) return;

        // Deactivate the current weapon if it is active
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Set the other weapon active
        currentWeapon = weapons[index];
        currentWeapon.SetActive(true);
    }

    public void DropWeapon() // Don't forget to put ammo too
    {
        if (currentWeapon == null) return;

        // Remove the current weapon from the inventory
        weapons.Remove(currentWeapon);

        // Spawn the weapon pickup in the world
        GameObject worldPickup = Instantiate(currentWeapon.GetComponent<Weapon>().weaponData.pickupPrefab, PlayerManager.instance.weaponHolder.position, Quaternion.Euler(Vector3.zero), null);
        worldPickup.transform.SetParent(null);

        // Remove weapon from hand and inventory
        Destroy(currentWeapon);
        currentWeapon = null;

        // Equip the first weapon when the other has been dropped
        if (weapons.Count != 0) EquipWeapon(0); 
    }

    public void SwapWeapon(WeaponData data)
    {
        // Remove weapon from inventory
        DropWeapon();

        // Add the other weapon to inventory and into the player's hands
        GameObject newWeapon = Instantiate(data.equipPrefab, PlayerManager.instance.weaponHolder);
        newWeapon.GetComponent<Weapon>().weaponData = data;
        weapons.Add(newWeapon);
        EquipWeapon(1);
    }

    // Sets the bullet that the weapon should use from the inventory
    public int SelectAmmo(BulletType type)
    {
        if (type == BulletType.small) return smallAmmo;
        else if (type == BulletType.medium) return mediumAmmo;
        else if (type == BulletType.shell) return shotgunShells;
        else return 0;
    }

    // Updates the ammmo used by the weapons
    public void UpdateAmmo(BulletType type, int ammo)
    {
        if (type == BulletType.small) smallAmmo = ammo;
        else if (type == BulletType.medium) mediumAmmo = ammo;
        else if (type == BulletType.shell) shotgunShells = ammo;
        else return;
    }
}
