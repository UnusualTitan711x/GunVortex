using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void UpdateHealthBar(int currentValue,int maxValue)
    {
        slider.value = currentValue / (float) maxValue;
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }
}
