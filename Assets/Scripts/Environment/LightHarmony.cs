using UnityEngine;

public class LightHarmony : MonoBehaviour
{
    private bool activated = false;
    private AudioSource audioSource;

    public SkyProgressionController progressionController;

    [Header("Particles")]
    public ParticleSystem[] particleSystems;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Stop emission at start
        SetEmission(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            ActivateHarmony();
        }
    }

    void ActivateHarmony()
    {
        activated = true;

        // Enable emission
        SetEmission(true);

        if (progressionController != null)
        {
            progressionController.RegisterHarmony();
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        Debug.Log("Harmony activated.");
    }

    void SetEmission(bool state)
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            var emission = ps.emission;
            emission.enabled = state;
        }
    }
}