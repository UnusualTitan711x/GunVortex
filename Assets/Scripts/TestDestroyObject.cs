using UnityEngine;

public class TestDestroyObject : MonoBehaviour, IInteractable, IDamagable
{
    public int maxHealth, health;
    public HealthBar healthBar;

    public void Interact()
    {
        Destroy(this.gameObject);
    }

    // Initiate health bar from Awake
    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Fixing the health bar at the start of your game
    void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Function to take damage from the bullets
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
