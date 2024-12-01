using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteractable
{
    PlayerInventory inventory = PlayerManager.instance.GetComponent<PlayerInventory>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Picked up weapon");
        Destroy(gameObject);
        
        //Make some checks to see if you can pick up the weapon
        if (inventory.weapons.Count < 2)
        {

        }
    }
}
