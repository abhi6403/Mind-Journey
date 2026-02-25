using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OceanEnvironmentController : MonoBehaviour
{
    [Header("Lighting")]
    public Light2D globalLight;
    public Color sadColor = new Color(0.3f, 0.5f, 0.7f);
    public Color calmColor = new Color(0.7f, 0.85f, 1f);

    [Header("Water")]
    public SpriteRenderer waterOverlay;

    [Header("Audio")]
    public AudioSource ambientSource;
    public AudioClip sadAmbience;
    public AudioClip calmAmbience;

    private int releaseCount = 0;
    public int releasesNeeded = 3;
    
    public AscentZone ascentZone;


    void Start()
    {
        SetSadState();
    }

    void SetSadState()
    {
        if (globalLight != null)
            globalLight.color = sadColor;

        if (ambientSource != null && sadAmbience != null)
        {
            ambientSource.clip = sadAmbience;
            ambientSource.Play();
        }
    }

    public void OnMemoryReleased()
    {
        releaseCount++;

        float progress = (float)releaseCount / releasesNeeded;

        if (releaseCount >= releasesNeeded && ascentZone != null)
        {
            ascentZone.ActivateZone();
        }

        UpdateLighting(progress);
        UpdateWater(progress);
        UpdateAudio(progress);
    }

    void UpdateLighting(float progress)
    {
        if (globalLight != null)
            globalLight.color = Color.Lerp(sadColor, calmColor, progress);
    }

    void UpdateWater(float progress)
    {
        if (waterOverlay != null)
        {
            Color c = waterOverlay.color;
            c.a = Mathf.Lerp(0.5f, 0.1f, progress);
            waterOverlay.color = c;
        }
    }

    void UpdateAudio(float progress)
    {
        if (progress > 0.6f && calmAmbience != null)
        {
            ambientSource.clip = calmAmbience;
            ambientSource.Play();
        }
    }
}