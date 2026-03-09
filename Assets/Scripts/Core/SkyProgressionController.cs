using System;
using UnityEngine;

public class SkyProgressionController : MonoBehaviour
{
    [Header("Progress Settings")]
    public int harmoniesNeeded = 3;
    private int harmonyCount = 0;

    [Header("Unlockables")]
    public GameObject newWindPath;
    public GameObject finalAscentWind;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip harmonyActivateSound;
    public AudioClip finalAscentSound;

    public void RegisterHarmony()
    {
        harmonyCount++;

        Debug.Log("Harmony activated count: " + harmonyCount);

        // Play harmony sound
        if (audioSource != null && harmonyActivateSound != null)
        {
            audioSource.PlayOneShot(harmonyActivateSound);
        }

        if (harmonyCount == 1 && newWindPath != null)
        {
            newWindPath.SetActive(true);
            Debug.Log("New wind path unlocked.");
        }

        if (harmonyCount >= harmoniesNeeded && finalAscentWind != null)
        {
            finalAscentWind.SetActive(true);
            Debug.Log("Final ascent wind activated.");

            // Play final ascent sound
            if (audioSource != null && finalAscentSound != null)
            {
                audioSource.PlayOneShot(finalAscentSound);
            }
        }
    }
}