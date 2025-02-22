using UnityEngine;

public class TestDestroyObject : MonoBehaviour, IDamagable
{
    public int maxHealth, health;
    public HealthBar healthBar;

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
