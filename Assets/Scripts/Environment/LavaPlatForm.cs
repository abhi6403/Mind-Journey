using UnityEngine;

public class LavaPlatform : MonoBehaviour
{
    public HeatSystem heatSystem;
    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (heatSystem.heatLevel > 0.7f)
        {
            col.enabled = false;
            sr.color = Color.red;
        }
        else
        {
            col.enabled = true;
            sr.color = Color.white;
        }
    }
}