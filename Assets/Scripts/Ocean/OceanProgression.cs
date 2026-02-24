using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OceanProgression : MonoBehaviour
{
    public Light2D globalLight;

    public Color sadColor = new Color(0.2f, 0.3f, 0.5f);
    public Color calmColor = new Color(0.6f, 0.7f, 0.9f);

    public int totalReleasesRequired = 3;
    private int releasedCount = 0;

    public void RegisterRelease()
    {
        releasedCount++;

        float progress = (float)releasedCount / totalReleasesRequired;
        globalLight.color = Color.Lerp(sadColor, calmColor, progress);

        Debug.Log("Ocean calming progress: " + progress);

        if (releasedCount >= totalReleasesRequired)
        {
            Debug.Log("Ocean Fully Calm");
        }
    }
}