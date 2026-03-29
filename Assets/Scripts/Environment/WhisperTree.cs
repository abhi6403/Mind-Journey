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

    [Header("Audio Clips")]
    public AudioClip triggerEnterClip;
    public AudioClip healingClip;
    public AudioClip healedClip;

    private Animator animator;
    public AudioSource whisperAudio;
    private bool playerInside = false;
    private bool healed = false;

    private SpriteRenderer appearRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (objectToAppear != null)
        {
            appearRenderer = objectToAppear.GetComponent<SpriteRenderer>();

            Color color = appearRenderer.color;
            color.a = 0f;
            appearRenderer.color = color;
        }

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

            if (triggerEnterClip != null)
                whisperAudio.PlayOneShot(triggerEnterClip);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !healed)
        {
            playerInside = false;
            animator.SetBool("playerNear", false);
        }
    }

    public void Interact()
    {
        if (playerInside && !healed)
        {
            if (healingClip != null)
                whisperAudio.PlayOneShot(healingClip);

            HealTree();
        }
    }

    void HealTree()
    {
        healed = true;

        animator.SetBool("healed", true);

        if (healedClip != null)
            whisperAudio.PlayOneShot(healedClip);

        if (progressionManager != null)
            progressionManager.ObjectHealed();

        if (objectToAppear != null)
            StartCoroutine(FadeInObject());
    }

    IEnumerator FadeInObject()
    {
        float timer = 0f;
        Color color = appearRenderer.color;

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

        color.a = 1f;
        appearRenderer.color = color;

        if (fadeParticles != null)
        {
            fadeParticles.Stop();
            fadeParticles.gameObject.SetActive(false);
        }
    }
}
