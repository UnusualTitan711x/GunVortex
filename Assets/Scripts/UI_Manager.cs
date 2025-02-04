using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager: MonoBehaviour
{
    public static UI_Manager instance {get; private set;}

    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;

    public Button interactButton;
    public Button switchButton;
    public Button fireButton;
    public Button reloadButton;

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
