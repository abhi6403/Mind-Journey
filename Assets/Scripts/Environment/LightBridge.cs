using UnityEngine;

public class LightBridge : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D col;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();

        col.enabled = false;
    }

    public void ActivateBridge()
    {
        animator.SetBool("activate", true);
        Debug.Log("Bridge activated");
        col.enabled = true;
    }
}