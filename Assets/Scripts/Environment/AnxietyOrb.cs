using UnityEngine;

public class AnxietyOrb : MonoBehaviour, IInteractable
{
    public ForestProgressionManager progressionManager;

    private Animator animator;
    private AudioSource audioSource;
    private bool playerNear = false;
    private bool isHealing = false;

    [Header("Healing Settings")]
    public float dissolveSpeed = 1f;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isHealing)
        {
            Color c = sr.color;
            c.a -= Time.deltaTime * dissolveSpeed;
            sr.color = c;

            if (c.a <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            animator.SetBool("playerNear", true);
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            animator.SetBool("playerNear", false);
            audioSource.Stop();
        }
    }

    public void Interact()
    {
        if (playerNear && !isHealing)
        {
            CalmOrb();
        }
    }

    void CalmOrb()
    {
        isHealing = true;
        animator.SetBool("healing", true);
        Debug.Log("Healed ,  anxietyOrb");
        progressionManager.ObjectHealed();
        audioSource.Stop();
    }
}