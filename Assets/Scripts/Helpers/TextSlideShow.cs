using System.Collections;
using UnityEngine;
using TMPro;

public class TextSlideshow : MonoBehaviour
{
    [Header("Text UI")]
    public TextMeshProUGUI textObject;

    [Header("Texts")]
    [TextArea]
    public string[] messages;

    [Header("Timing")]
    public float textDuration = 3f;
    public float fadeDuration = 1f;

    [Header("Final Unlock")]
    public GameObject objectToUnlock;

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlaySlideshow());
    }

    IEnumerator PlaySlideshow()
    {
        while (currentIndex < messages.Length)
        {
            textObject.text = messages[currentIndex];

            // Fade in
            yield return StartCoroutine(FadeText(0, 1));

            // If this is the last text
            if (currentIndex == messages.Length - 1)
            {
                if (objectToUnlock != null)
                    objectToUnlock.SetActive(true);

                yield break; // stop slideshow (final text stays)
            }

            // Wait before fade out
            yield return new WaitForSeconds(textDuration);

            // Fade out
            yield return StartCoroutine(FadeText(1, 0));

            currentIndex++;
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = textObject.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            textObject.color = new Color(c.r, c.g, c.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        textObject.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}