using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {get; private set;}

    public Transform weaponHolder;
    public PlayerInventory inventory;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
