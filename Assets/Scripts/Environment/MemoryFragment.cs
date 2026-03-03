using System;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public float weightValue = 0.2f;

    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("Memory collected.");

            UnderwaterMemory memory = other.GetComponent<UnderwaterMemory>();
            if (memory != null)
            {
                memory.AddMemory(weightValue);
            }

            Destroy(gameObject);
        }
    }
}