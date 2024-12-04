using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Objects/Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float reloadTime;
    public int magazineSize;
    public int bulletCount;
    public int capacity;
    public bool isAutomatic;
    public float fireRate;
    public float spread;
    public GameObject pickupPrefab;
    public GameObject equipPrefab;
    public GameObject bulletPrefab;

}
