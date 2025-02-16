using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth, health;
    public float detectionRange = 10;
    public float rotationSpeed = 10;
    public int damage = 2;
    public float fireRate = 0.7f;
    public float spreadAngle = 10;
    float fireTimer;
    public HealthBar healthBar;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] Transform muzzle;
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
        player = PlayerManager.instance.player.transform;
    }

    // Function to take damage from the bullets
    public void TakeDamage(int damage)
    {
        FollowPlayer();

        if (health > damage) 
        {
            health -= damage;
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        else 
        {
            UI_Manager.instance.enemyCounter++;
            UI_Manager.instance.UpdateKillCount();
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            if (fireTimer <= 0)
            {
                Shoot();
            }
            else 
            {
                fireTimer -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {   
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
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.fixedDeltaTime);
        agent.SetDestination(player.transform.position);
    }

    void Shoot()
    {
        fireTimer = fireRate;

        float spread = Random.Range(-spreadAngle, spreadAngle);
        Vector3 spreadDirection = Quaternion.Euler(0, 0, spread) * muzzle.up;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(Vector3.forward, spreadDirection));

        bullet.GetComponent<Bullet>().damage = damage;
    }
}
