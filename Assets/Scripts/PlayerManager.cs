using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Transform weaponHolder;

    void Awake()
    {
        instance = this;
    }
}
