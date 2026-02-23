using UnityEngine;

public class WaterZone : MonoBehaviour
{
    public OceanMovementModifier oceanModifier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oceanModifier.EnterWater();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oceanModifier.ExitWater();
        }
    }
}