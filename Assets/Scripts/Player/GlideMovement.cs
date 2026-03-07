using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GlideMovement : MonoBehaviour
{
    [Header("Glide Settings")]
    public float moveSpeed = 5f;
    public float glideGravity = 0.2f;
    public float liftForce = 4f;
    public float maxFallSpeed = 2f;

    private Rigidbody2D rb;

    [Header("Mobile Button")]
    public Button upButton;

    private bool mobileUp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Reduce gravity for float feeling
        rb.gravityScale = glideGravity;

        SetupButton();
    }

    void SetupButton()
    {
        if (upButton == null) return;

        EventTrigger trigger = upButton.gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = upButton.gameObject.AddComponent<EventTrigger>();

        // BUTTON PRESS
        EventTrigger.Entry down = new EventTrigger.Entry();
        down.eventID = EventTriggerType.PointerDown;
        down.callback.AddListener((data) => { mobileUp = true; });

        // BUTTON RELEASE
        EventTrigger.Entry up = new EventTrigger.Entry();
        up.eventID = EventTriggerType.PointerUp;
        up.callback.AddListener((data) => { mobileUp = false; });

        trigger.triggers.Add(down);
        trigger.triggers.Add(up);
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

        // Keyboard glide
        if (Input.GetKey(KeyCode.Space))
        {
            MoveUp();
        }

        // Mobile glide
        if (mobileUp)
        {
            MoveUp();
        }
    }

    void LimitFallSpeed()
    {
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }

    void MoveUp()
    {
        rb.AddForce(Vector2.up * liftForce);
    }
}
