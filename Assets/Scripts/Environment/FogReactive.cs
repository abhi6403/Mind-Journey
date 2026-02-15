using UnityEngine;

public class FogReactive : MonoBehaviour, IInteractable
{
    public ForestProgressionManager progressionManager;
    
    private SpriteRenderer sr;
    private bool isClearing = false;

    [Header("Fog Settings")]
    public float fadeSpeed = 1.5f;
    public float minAlpha = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isClearing)
        {
            Color c = sr.color;
            c.a = Mathf.Lerp(c.a, minAlpha, Time.deltaTime * fadeSpeed);
            sr.color = c;

            if (c.a <= 0.05f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Interact()
    {
        isClearing = true;
        progressionManager.ObjectHealed();
    }
}