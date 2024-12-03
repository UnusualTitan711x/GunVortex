using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public float lifetime = 4;
    public int damage;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
