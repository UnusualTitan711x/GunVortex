using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 10f;

    private float mov_x, mov_y;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(mov_x, mov_y) * speed;
    }

    void GetInput()
    {
        mov_x = Input.GetAxisRaw("Horizontal");
        mov_y = Input.GetAxisRaw("Vertical");

    }
}
