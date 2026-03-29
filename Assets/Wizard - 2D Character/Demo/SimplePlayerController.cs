using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f;

        private Rigidbody2D rb;
        private Animator anim;

        private int direction = 1;
        private bool isJumping = false;
        private bool alive = true;

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
            anim = GetComponent<Animator>();

            SetupMobileButtons();
        }

        void SetupMobileButtons()
        {
            if (leftButton != null)
            {
                AddHoldEvent(leftButton.gameObject, MoveLeftDown, MoveStop);
            }

            if (rightButton != null)
            {
                AddHoldEvent(rightButton.gameObject, MoveRightDown, MoveStop);
            }

            if (jumpButton != null)
            {
                jumpButton.onClick.AddListener(JumpButton);
            }
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
            Restart();

            if (alive)
            {
                Hurt();
                Die();
                Attack();
                Jump();
                Run();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

            float horizontal = Input.GetAxisRaw("Horizontal") + mobileHorizontal;

            if (horizontal < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);

                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }

            if (horizontal > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);

                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }

            transform.position += moveVelocity * movePower * Time.deltaTime;
        }

        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || mobileJump) && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
                mobileJump = false;
            }

            if (!isJumping)
                return;

            rb.linearVelocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                anim.SetTrigger("attack");
        }

        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");

                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }

        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                anim.SetTrigger("die");
                alive = false;
            }
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
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
}
