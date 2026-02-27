using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightBridge : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D col;
    public BoxCollider2D box;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Level_Volcano");
    }
}