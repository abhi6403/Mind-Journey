using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnderwaterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 5f;
    public float maxSpeed = 3f;
    public float verticalForce = 4f;
    public float drag = 3f;

    private Rigidbody2D rb;

    // MOBILE INPUT
    private float mobileHorizontal = 0f;
    private float mobileVertical = 0f;

    [Header("Mobile Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make underwater physics feel heavy
        rb.gravityScale = 0.3f;
        rb.linearDamping = drag;

        SetupButtons();
    }

    void SetupButtons()
    {
        if (leftButton != null)
            AddHoldEvent(leftButton.gameObject, MoveLeftDown, StopHorizontal);

        if (rightButton != null)
            AddHoldEvent(rightButton.gameObject, MoveRightDown, StopHorizontal);

        if (upButton != null)
            AddHoldEvent(upButton.gameObject, MoveUpDown, StopVertical);

        if (downButton != null)
            AddHoldEvent(downButton.gameObject, MoveDownDown, StopVertical);
    }

    void AddHoldEvent(GameObject buttonObj, UnityEngine.Events.UnityAction downAction, UnityEngine.Events.UnityAction upAction)
    {
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();

        if (trigger == null)
            trigger = buttonObj.AddComponent<EventTrigger>();

        EventTrigger.Entry down = new EventTrigger.Entry();
        down.eventID = EventTriggerType.PointerDown;
        down.callback.AddListener((data) => { downAction(); });

        EventTrigger.Entry up = new EventTrigger.Entry();
        up.eventID = EventTriggerType.PointerUp;
        up.callback.AddListener((data) => { upAction(); });

        trigger.triggers.Add(down);
        trigger.triggers.Add(up);
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") + mobileHorizontal;
        float moveY = Input.GetAxis("Vertical") + mobileVertical;

        Vector2 force = new Vector2(moveX * moveForce, moveY * verticalForce);

        rb.AddForce(force);

        // Limit speed
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    // MOBILE INPUT FUNCTIONS

    public void MoveLeftDown()
    {
        mobileHorizontal = -1;
    }

    public void MoveRightDown()
    {
        mobileHorizontal = 1;
    }

    public void StopHorizontal()
    {
        mobileHorizontal = 0;
    }

    public void MoveUpDown()
    {
        mobileVertical = 1;
    }

    public void MoveDownDown()
    {
        mobileVertical = -1;
    }

    public void StopVertical()
    {
        mobileVertical = 0;
    }
}
