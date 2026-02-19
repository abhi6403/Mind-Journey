using UnityEngine;
using System.Collections;

public class WhisperTree : MonoBehaviour, IInteractable
{
    public ForestProgressionManager progressionManager;

    [Header("Fade Object")]
    public GameObject objectToAppear;
    public float fadeDuration = 2f;

    [Header("Fade Particle Effect")]
    public ParticleSystem fadeParticles;

    private Animator animator;
    private AudioSource whisperAudio;
    private bool playerInside = false;
    private bool healed = false;

    private SpriteRenderer appearRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        whisperAudio = GetComponent<AudioSource>();

        // 🔹 Setup object invisibility
        if (objectToAppear != null)
        {
            appearRenderer = objectToAppear.GetComponent<SpriteRenderer>();

            Color color = appearRenderer.color;
            color.a = 0f;
            appearRenderer.color = color;
        }

        // 🔹 Make sure particles are OFF at start
        if (fadeParticles != null)
        {
            fadeParticles.Stop();
            fadeParticles.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !healed)
        {
            playerInside = true;
            animator.SetBool("playerNear", true);
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

        whisperAudio.Stop();
        progressionManager.ObjectHealed();

        if (objectToAppear != null)
        {
            StartCoroutine(FadeInObject());
        }
    }

    IEnumerator FadeInObject()
    {
        float timer = 0f;
        Color color = appearRenderer.color;

        // 🔥 Turn ON particles when fade starts
        if (fadeParticles != null)
        {
            fadeParticles.gameObject.SetActive(true);
            fadeParticles.Play();
        }

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            color.a = alpha;
            appearRenderer.color = color;

            yield return null;
        }

        // Ensure fully visible
        color.a = 1f;
        appearRenderer.color = color;

        // 🔥 Turn OFF particles after fade completes
        if (fadeParticles != null)
        {
            fadeParticles.Stop();
            fadeParticles.gameObject.SetActive(false);
        }
    }
}
