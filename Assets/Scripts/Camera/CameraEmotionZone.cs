using UnityEngine;
using Unity.Cinemachine;

public class CameraEmotionZone : MonoBehaviour
{
    public CinemachineCamera vcam;
    public float targetZoom = 5f;
    public float transitionSpeed = 2f;

    private bool playerInside = false;

    void Update()
    {
        if (playerInside)
        {
            vcam.Lens.OrthographicSize = Mathf.Lerp(
                vcam.Lens.OrthographicSize,
                targetZoom,
                Time.deltaTime * transitionSpeed
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }
}