using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public GameObject endingText;
    public float textDelay = 3f;

    void Start()
    {
        Invoke("ShowEndingText", textDelay);
    }

    void ShowEndingText()
    {
        if (endingText != null)
        {
            endingText.SetActive(true);
        }
    }
}