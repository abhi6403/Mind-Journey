using UnityEngine;

public class GlideMovement : MonoBehaviour
{
    [Header("Glide Settings")]
    public float moveSpeed = 5f;
    public float glideGravity = 0.2f;
    public float liftForce = 4f;
    public float maxFallSpeed = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Reduce gravity for float feeling
        rb.gravityScale = glideGravity;
    }

    void Update()
    {
        HandleMovement();
        LimitFallSpeed();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Hold jump to glide upward slightly
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * liftForce);
        }
    }

    void LimitFallSpeed()
    {
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }
}