using UnityEngine;

public class AscentZone : MonoBehaviour
{
    public float upwardForce = 5f;

    private bool active = false;
    public UnderwaterMemory memory;

    public void ActivateZone()
    {
        active = true;
        gameObject.SetActive(true);
        Debug.Log("Ascent zone activated.");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!active) return;

        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                memory.EnableParticles();
                rb.AddForce(Vector2.up * upwardForce);
            }
        }
    }
}