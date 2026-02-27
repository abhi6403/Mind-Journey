using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EmotionEnvironmentController : MonoBehaviour
{
    [Header("Lighting")]
    public Light2D globalLight;
    public Color anxiousColor = new Color(0.4f, 0.5f, 0.7f);
    public Color calmColor = new Color(0.8f, 0.75f, 0.6f);
    
    [Header("Fog Parent")]
    public GameObject fogGroup;

    [Header("Audio")]
    public AudioSource ambientSource;
    public AudioClip anxiousAmbience;
    public AudioClip calmAmbience;

    [Header("Progress Settings")]
    public int totalHealTargets = 13;

    private int healedCount = 0;
    private bool calmAudioPlayed = false;

    void Start()
    {
        SetAnxietyState();
    }

    public void OnObjectHealed(int currentHealCount)
    {
        healedCount = currentHealCount;

        float progress = Mathf.Clamp01((float)healedCount / totalHealTargets);

        UpdateLighting(progress);
        UpdateAudio(progress);
        UpdateFog(progress);
    }

    void SetAnxietyState()
    {
        if (globalLight != null)
            globalLight.color = anxiousColor;

        if (ambientSource != null && anxiousAmbience != null)
        {
            ambientSource.clip = anxiousAmbience;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }

    void UpdateLighting(float progress)
    {
        if (globalLight != null)
            globalLight.color = Color.Lerp(anxiousColor, calmColor, progress);
    }

    void UpdateAudio(float progress)
    {
        if (ambientSource == null) return;

        if (progress > 0.6f && !calmAudioPlayed)
        {
            calmAudioPlayed = true;

            if (calmAmbience != null)
            {
                ambientSource.clip = calmAmbience;
                ambientSource.loop = true;
                ambientSource.Play();
            }
        }
    }

    void UpdateFog(float progress)
    {
        if (fogGroup != null)
        {
            fogGroup.transform.localScale = Vector3.Lerp(
                new Vector3(1.2f, 1.2f, 1f),
                new Vector3(0.8f, 0.8f, 1f),
                progress
            );
        }
    }
}
