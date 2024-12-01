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
        if (index < 0 || index > 2)
        {
            return;
        }

        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        currentWeapon = weapons[index];
        currentWeapon.SetActive(true);
    }

    public void DropWeapon()
    {
        
    }
}
