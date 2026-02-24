using ClearSky;
using UnityEngine;

public class OceanMovementModifier : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public SimplePlayerController playerController;

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
        normalMoveSpeed = playerController.movePower;
        normalJumpForce = playerController.jumpPower;
    }

    public void EnterWater()
    {
        playerRb.gravityScale = underwaterGravityScale;
        playerController.movePower = underwaterMoveSpeed;
        playerController.jumpPower = underwaterJumpForce;

        Debug.Log("Entered Water - Heavy Movement Applied");
    }

    public void ExitWater()
    {
        playerRb.gravityScale = normalGravity;
        playerController.movePower = normalMoveSpeed;
        playerController.jumpPower = normalJumpForce;

        Debug.Log("Exited Water - Normal Movement Restored");
    }
}