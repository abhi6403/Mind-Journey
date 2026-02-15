using UnityEngine;

public class WhisperTree : MonoBehaviour, IInteractable
{
    public ForestProgressionManager progressionManager;
    
    private Animator animator;
    private AudioSource whisperAudio;
    private bool playerInside = false;
    private bool healed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        whisperAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !healed)
        {
            playerInside = true;
            animator.SetBool("playerNear", true);
            Debug.Log("player is near ");
            whisperAudio.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !healed)
        {
            playerInside = false;
            animator.SetBool("playerNear", false);
            whisperAudio.Stop();
        }
    }

    public void Interact()
    {
        if (playerInside && !healed)
        {
            HealTree();
        }
    }

    void HealTree()
    {
        healed = true;
        animator.SetBool("healed", true);
        Debug.Log("Healed");
        progressionManager.ObjectHealed();
        whisperAudio.Stop();
    }
}