using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float lifetime = 4;
    public int damage;
    Rigidbody2D rb;
    Vector2 playerVelocity, bulletDirection, finalVelocity;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
       
        playerVelocity = PlayerManager.instance.player.GetComponent<Rigidbody2D>().linearVelocity;
        bulletDirection = transform.up;
        finalVelocity = playerVelocity + bulletDirection * speed;
        rb.linearVelocity = finalVelocity.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
