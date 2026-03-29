using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    private Animator animator;
    public string sceneToLoad;
    private BoxCollider2D col;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    public void ActivateBridge()
    {
        animator.SetBool("activate", true);
        Debug.Log("Bridge activated");
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
