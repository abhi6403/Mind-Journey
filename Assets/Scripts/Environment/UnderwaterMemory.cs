using UnityEngine;

public class UnderwaterMemory : MonoBehaviour
{
    [Header("Memory Settings")]
    public float currentWeight = 0f;
    public float maxWeight = 2f;

    [Header("Release Settings")]
    public GameObject releaseParticlePrefab;
    public Transform releasePoint;

    private UnderwaterMovement movement;

    public OceanEnvironmentController environmentController;

    void Start()
    {
        movement = GetComponent<UnderwaterMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReleaseMemories();
        }
    }

    public void AddMemory(float amount)
    {
        currentWeight += amount;
        currentWeight = Mathf.Clamp(currentWeight, 0f, maxWeight);

        Debug.Log("Current emotional weight: " + currentWeight);

        ApplyWeightEffect();
    }

    void ApplyWeightEffect()
    {
        if (movement != null)
        {
            movement.maxSpeed = Mathf.Lerp(3f, 1.2f, currentWeight / maxWeight);
            movement.moveForce = Mathf.Lerp(5f, 2f, currentWeight / maxWeight);
        }
    }

    void ReleaseMemories()
    {
        if (currentWeight <= 0f) return;

        Debug.Log("Releasing memories...");

        // spawn visual particles
        if (releaseParticlePrefab != null)
        {
            Instantiate(releaseParticlePrefab, releasePoint.position, Quaternion.identity);
        }

        if (environmentController != null)
        {
            environmentController.OnMemoryReleased();
        }

        // reset weight
        currentWeight = 0f;

        ApplyWeightEffect();
    }
}