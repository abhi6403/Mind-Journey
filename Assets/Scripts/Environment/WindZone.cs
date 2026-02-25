using UnityEngine;

public class WindZone : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector2 windDirection = Vector2.up;
    public float windStrength = 8f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(windDirection * windStrength);
            }
        }
    }
}