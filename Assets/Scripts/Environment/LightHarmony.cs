using UnityEngine;

public class LightHarmony : MonoBehaviour
{
    private bool activated = false;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    public SkyProgressionController progressionController;

    [Header("Visual Settings")]
    public Color inactiveColor = new Color(1f, 1f, 1f, 0.4f);
    public Color activeColor = new Color(1f, 0.9f, 0.6f, 1f);

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        sr.color = inactiveColor;
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

        sr.color = activeColor;

        if (progressionController != null)
        {
            progressionController.RegisterHarmony();
        }

        if (audioSource != null)
            audioSource.Play();

        Debug.Log("Harmony activated.");
    }
}