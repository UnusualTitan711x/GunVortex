using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth, health;
    public float speed;
    float detectionRange = 10;
    public HealthBar healthBar;
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private Transform graphic;
    NavMeshAgent agent;

    // Initiate health bar from Awake
    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Fixing the health bar at the start of your game
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        healthBar.UpdateHealthBar(health, maxHealth);

        agent.updateRotation = false;
        agent.updateUpAxis = false;
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

    void FixedUpdate()
    {
        player = PlayerManager.instance.player.transform;
        
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            FollowPlayer();
        }
        else 
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void FollowPlayer()
    {
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        agent.SetDestination(player.transform.position);
    }
}
