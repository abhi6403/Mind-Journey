using UnityEngine;

public class LavaHeatVisual : MonoBehaviour
{
    [Header("References")]
    public HeatSystem heatSystem;
    public SpriteRenderer lavaRenderer;

    [Header("Lava Colors")]
    public Color coolLavaColor = new Color(0.5f, 0.2f, 0.2f);
    public Color hotLavaColor = new Color(1f, 0.3f, 0.1f);

    void Start()
    {
        lavaRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (heatSystem == null || lavaRenderer == null)
            return;

        float heat = heatSystem.GetHeatLevel();

        lavaRenderer.color = Color.Lerp(coolLavaColor, hotLavaColor, heat);
    }
}