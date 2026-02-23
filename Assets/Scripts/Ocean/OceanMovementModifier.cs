using UnityEngine;

public class OceanMovementModifier : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public PlayerController playerController;

    [Header("Underwater Settings")]
    public float underwaterGravityScale = 0.5f;
    public float underwaterMoveSpeed = 2f;
    public float underwaterJumpForce = 3f;

    private float normalGravity;
    private float normalMoveSpeed;
    private float normalJumpForce;

    void Start()
    {
        normalGravity = playerRb.gravityScale;
        normalMoveSpeed = playerController.moveSpeed;
        normalJumpForce = playerController.jumpForce;
    }

    public void EnterWater()
    {
        playerRb.gravityScale = underwaterGravityScale;
        playerController.moveSpeed = underwaterMoveSpeed;
        playerController.jumpForce = underwaterJumpForce;

        Debug.Log("Entered Water - Heavy Movement Applied");
    }

    public void ExitWater()
    {
        playerRb.gravityScale = normalGravity;
        playerController.moveSpeed = normalMoveSpeed;
        playerController.jumpForce = normalJumpForce;

        Debug.Log("Exited Water - Normal Movement Restored");
    }
}