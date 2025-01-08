using UnityEngine;
using TMPro;

public class UI_Manager: MonoBehaviour
{
    public static UI_Manager instance {get; private set;}

    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;

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
