using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Manager: MonoBehaviour
{
    public static UI_Manager instance {get; private set;}

    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;
    public TextMeshProUGUI enemyCounterText;

    public Button interactButton;
    public Button switchButton;
    public Button fireButton;
    public Button reloadButton;

    public List<GameObject> weaponIndicator;

    public int enemyCounter = 0;

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

    void Start()
    {
        UpdateKillCount();
    }

    public void UpdateKillCount()
    {
        enemyCounterText.SetText(enemyCounter.ToString());
    }
}
