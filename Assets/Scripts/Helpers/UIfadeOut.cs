using UnityEngine;
using System.Collections;

public class UIFadeOut : MonoBehaviour
{
    public float fadeDuration = 2f;
    public bool disableAfterFade = true;

    private CanvasGroup canvasGroup;
    private bool isFading = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;

        float timer = 0f;
        float startAlpha = canvasGroup.alpha;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);

            yield return null;
        }

        canvasGroup.alpha = 0f;

        if (disableAfterFade)
            gameObject.SetActive(false);
    }
}