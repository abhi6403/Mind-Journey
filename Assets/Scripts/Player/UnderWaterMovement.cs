using UnityEngine;

public class UnderwaterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 5f;
    public float maxSpeed = 3f;
    public float verticalForce = 4f;
    public float drag = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make underwater physics feel heavy
        rb.gravityScale = 0.3f;
        rb.linearDamping = drag;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 force = new Vector2(moveX * moveForce, moveY * verticalForce);
        rb.AddForce(force);

        // Limit speed
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
}