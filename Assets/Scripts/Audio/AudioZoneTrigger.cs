using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    public AudioSource ambientSource;
    public AudioClip zoneClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ambientSource.clip = zoneClip;
            ambientSource.Play();
        }
    }
}