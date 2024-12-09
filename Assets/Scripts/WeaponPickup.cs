using System.Data.Common;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteractable
{
    public WeaponData data;
    
    public int ammo = 0; // Ammo contained in the pickup. maybe after dropping 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact()
    {        
        PlayerManager.instance.inventory.AddWeapon(data);
        Destroy(gameObject);
    }
}
