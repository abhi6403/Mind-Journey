using UnityEngine;

public class OceanMovementModifier : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D playerRb;
    public GameObject playerObject;

    private PlayerController playerController;

    [Header("Underwater Base Settings")]
    public float underwaterGravityScale = 0.5f;
    public float underwaterMoveSpeed = 2f;
    public float underwaterJumpForce = 3f;

    [Header("Emotional Weight Settings")]
    public float extraGravityPerFragment = 0.2f;
    public float extraSlowPerFragment = 0.3f;

    private float normalGravity;
    private float normalMoveSpeed;
    private float normalJumpForce;

    private int collectedFragments = 0;
    private bool isUnderwater = false;

    void Start()
    {
        playerController = playerObject.GetComponent<PlayerController>();

        normalGravity = playerRb.gravityScale;
        normalMoveSpeed = playerController.moveSpeed;
        normalJumpForce = playerController.jumpForce;
    }

    // =============================
    // WATER STATE CONTROL
    // =============================

    public void EnterWater()
    {
        isUnderwater = true;

        playerRb.gravityScale = underwaterGravityScale + (collectedFragments * extraGravityPerFragment);
        playerController.moveSpeed = underwaterMoveSpeed - (collectedFragments * extraSlowPerFragment);
        playerController.jumpForce = underwaterJumpForce;

        Debug.Log("Entered Water - Heavy Movement Applied");
    }

    public void ExitWater()
    {
        isUnderwater = false;

        playerRb.gravityScale = normalGravity;
        playerController.moveSpeed = normalMoveSpeed;
        playerController.jumpForce = normalJumpForce;

        Debug.Log("Exited Water - Normal Movement Restored");
    }

    // =============================
    // FRAGMENT WEIGHT SYSTEM
    // =============================

    public void AddFragmentWeight()
    {
        collectedFragments++;

        if (isUnderwater)
        {
            playerRb.gravityScale += extraGravityPerFragment;
            playerController.moveSpeed -= extraSlowPerFragment;
        }

        Debug.Log("Fragment Added. Total Weight: " + collectedFragments);
    }

    public void RemoveFragmentWeight()
    {
        if (collectedFragments <= 0)
            return;

        collectedFragments--;

        if (isUnderwater)
        {
            playerRb.gravityScale -= extraGravityPerFragment;
            playerController.moveSpeed += extraSlowPerFragment;
        }

        Debug.Log("Fragment Released. Total Weight: " + collectedFragments);
    }

    void Update()
    {
        if (playerController.moveSpeed < 0.5f)
        {
            playerController.moveSpeed = 0.5f;
        }
    }
}