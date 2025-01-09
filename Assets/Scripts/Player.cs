using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    public Joystick movementJoystick;
    public Joystick rotationJoystick;

    public float speed = 6f;
    private float mov_x, mov_y;

    private Vector2 mousePos;
    public Vector2 rotationDirection;

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
        rb.linearVelocity = new Vector2(mov_x, mov_y) * speed * 100.0f * Time.fixedDeltaTime;
    }

    void GetInput()
    {
        rotationDirection = new Vector2(rotationJoystick.Horizontal, rotationJoystick.Vertical);

        // Getting mouse and keyboard input
        mov_x = movementJoystick.Horizontal;
        mov_y = movementJoystick.Vertical;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CharacterRotate()
    {
        // Calculate the angle the character should face and rotate accordingly
        float angle = Mathf.Atan2(rotationJoystick.Direction.y, rotationJoystick.Direction.x) * Mathf.Rad2Deg - 90f;
    
        if (rotationJoystick.Direction.magnitude >= .2f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
