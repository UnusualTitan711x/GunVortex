using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    
    void Awake()
    {
        // Assigns the slider for the health bar
        slider = gameObject.GetComponent<Slider>();
    }

    // Function to update the health bar according to the health left
    public void UpdateHealthBar(int currentValue,int maxValue)
    {
        slider.value = currentValue / (float) maxValue;
    }

    void Update()
    {
        // Set the health bar rotation to always face the camera and position to me fixed
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }
}
