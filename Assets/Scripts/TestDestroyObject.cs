using UnityEngine;

public class TestDestroyObject : MonoBehaviour, IInteractable, IDamagable
{
    public int maxHealth, health;
    public HealthBar healthBar;

    public void Interact()
    {
        Destroy(this.gameObject);
    }

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (health > damage) 
        {
            health -= damage;
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        else Destroy(this.gameObject);
    }
}
