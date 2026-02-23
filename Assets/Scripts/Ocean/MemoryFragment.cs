using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;

    private bool collected = false;

    void Update()
    {
        if (collected)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                player.position + new Vector3(0, 1f, 0),
                Time.deltaTime * followSpeed
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collected = true;
            Debug.Log("Memory Fragment Collected");
        }
    }
}