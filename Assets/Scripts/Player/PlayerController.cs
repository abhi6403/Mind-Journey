using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 6f;

    private Rigidbody2D rb;
    private bool isGrounded;

    // MOBILE INPUT
    private float mobileHorizontal = 0f;
    private bool mobileJump = false;

    [Header("Mobile Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetupButtons();
    }

    void SetupButtons()
    {
        if (leftButton != null)
            AddHoldEvent(leftButton.gameObject, MoveLeftDown, MoveStop);

        if (rightButton != null)
            AddHoldEvent(rightButton.gameObject, MoveRightDown, MoveStop);

        if (jumpButton != null)
            jumpButton.onClick.AddListener(JumpButton);
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
        Move();
        Jump();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal") + mobileHorizontal;

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || mobileJump) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            mobileJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
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

    public void MoveStop()
    {
        mobileHorizontal = 0;
    }

    public void JumpButton()
    {
        mobileJump = true;
    }
}
