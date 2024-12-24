using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 6f;
    private float mov_x, mov_y;

    private Vector2 mousePos;

    void Start()
    {
        // Initialise the rigidbody used by the player
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Always get input and rotate the character 
        GetInput();
        CharacterRotate();
    }

    void FixedUpdate()
    {
        // Set the character to move using physics
        rb.linearVelocity = new Vector2(mov_x, mov_y).normalized * speed * 100.0f * Time.deltaTime;
    }

    void GetInput()
    {
        // Getting mouse and keyboard input
        mov_x = Input.GetAxisRaw("Horizontal");
        mov_y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CharacterRotate()
    {
        // Calculate the angle the character should face and rotate accordingly
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
