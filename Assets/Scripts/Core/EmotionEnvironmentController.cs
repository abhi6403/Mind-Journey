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

    private int healedCount = 0;
    public int totalHealTargets = 3;

    void Start()
    {
        SetAnxietyState();
    }

    public void OnObjectHealed()
    {
        healedCount++;

        float progress = (float)healedCount / totalHealTargets;

        UpdateLighting(progress);
        UpdateAudio(progress);
        UpdateFog(progress);
    }

    void SetAnxietyState()
    {
        globalLight.color = anxiousColor;
        ambientSource.clip = anxiousAmbience;
        ambientSource.Play();
    }

    void UpdateLighting(float progress)
    {
        globalLight.color = Color.Lerp(anxiousColor, calmColor, progress);
    }

    void UpdateAudio(float progress)
    {
        if (progress > 0.6f)
        {
            ambientSource.clip = calmAmbience;
            ambientSource.Play();
        }
    }

    void UpdateFog(float progress)
    {
        if (fogGroup != null)
        {
            fogGroup.transform.localScale = Vector3.Lerp(
                new Vector3(1.2f,1.2f,1),
                new Vector3(0.8f,0.8f,1),
                progress
            );
        }
    }
}