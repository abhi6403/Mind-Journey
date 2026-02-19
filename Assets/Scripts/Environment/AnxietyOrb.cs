using UnityEngine;
using System.Collections;

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

    [Header("Particles")]
    public ParticleSystem unhealedParticles;
    public ParticleSystem healedParticles;
    public float fadeDuration = 1.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        // Start state
        unhealedParticles.gameObject.SetActive(true);
        healedParticles.gameObject.SetActive(false);
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
                sr.enabled = false;      // hide sprite only
                isHealing = false;       // stop update loop
                this.enabled = false;    // disable script
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
        progressionManager.ObjectHealed();
        audioSource.Stop();

        StartCoroutine(FadeParticles());
    }

    IEnumerator FadeParticles()
    {
        healedParticles.gameObject.SetActive(true);

        ParticleSystem.MainModule unhealedMain = unhealedParticles.main;
        ParticleSystem.MainModule healedMain = healedParticles.main;

        Color unhealedColor = unhealedMain.startColor.color;
        Color healedColor = healedMain.startColor.color;

        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            // fade OUT unhealed
            Color u = unhealedColor;
            u.a = Mathf.Lerp(1f, 0f, t);
            unhealedMain.startColor = u;

            // fade IN healed
            Color h = healedColor;
            h.a = Mathf.Lerp(0f, 1f, t);
            healedMain.startColor = h;

            yield return null;
        }

        // final state
        unhealedParticles.gameObject.SetActive(false);
    }
}
