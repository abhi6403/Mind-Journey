using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public float weightValue = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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