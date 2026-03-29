using UnityEngine;

public class FogClear : MonoBehaviour, IInteractable
{
    public float fadeSpeed = 2f;
    private SpriteRenderer sr;
    private bool isClearing = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isClearing)
        {
            Color c = sr.color;
            c.a -= Time.deltaTime * fadeSpeed;
            sr.color = c;

            if (c.a <= 0)
                gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        isClearing = true;
    }
}