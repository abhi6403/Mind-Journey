using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [Header("Light Settings")]
    public GameObject lightPulsePrefab;
    public Transform lightSpawnPoint;
    public float lightRadius = 3f;
    public LayerMask interactLayer;
    public AudioClip lightSound;
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EmitLight();
        }
    }

    public void EmitLight()
    {
        if (lightSound != null)
        {
            audioSource.PlayOneShot(lightSound);
        }
        // Spawn visual light pulse
        Instantiate(lightPulsePrefab, lightSpawnPoint.position, Quaternion.identity);

        // Detect nearby interactable objects
        Collider2D[] hits = Physics2D.OverlapCircleAll(lightSpawnPoint.position, lightRadius, interactLayer);

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    // Visualize radius in editor
    void OnDrawGizmosSelected()
    {
        if (lightSpawnPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(lightSpawnPoint.position, lightRadius);
    }
}