using UnityEngine;

public class RagePillar : MonoBehaviour
{
    [Header("References")]
    public GameObject fireObject;   // parent object of particle system
    public HeatSystem heatSystem;

    public GameObject blocker;
    
    [Header("Calm Settings")]
    public float calmTimeRequired = 2f;
    private float calmTimer = 0f;
    private bool playerInside = false;
    private bool pillarCalmed = false;

    void Start()
    {
        // Fire OFF at start
        if (fireObject != null)
        {
            fireObject.SetActive(false);
            Debug.Log("Fire disabled at start.");
        }
    }

    void Update()
    {
        if (playerInside && !pillarCalmed)
        {
            float playerSpeed = Mathf.Abs(heatSystem.playerRb.linearVelocity.x);

            if (playerSpeed < 0.1f)
            {
                calmTimer += Time.deltaTime;
                Debug.Log("Player waiting... Time: " + calmTimer.ToString("F2"));

                if (calmTimer >= calmTimeRequired)
                {
                    Debug.Log("Player wait time completed. Calming pillar.");
                    blocker.SetActive(false);
                    CalmPillar();
                }
            }
            else
            {
                Debug.Log("Player moving — calm reset.");
                calmTimer = 0f;
            }
        }
    }

    void CalmPillar()
    {
        pillarCalmed = true;
        heatSystem.RegisterCalmEvent();

        if (fireObject != null)
        {
            fireObject.SetActive(false);
            Debug.Log("Fire disabled. Pillar calmed.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pillarCalmed)
        {
            playerInside = true;
            Debug.Log("Player entered rage pillar zone.");

            if (fireObject != null)
            {
                fireObject.SetActive(true);
                Debug.Log("Fire activated.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            calmTimer = 0f;
            Debug.Log("Player exited rage pillar zone.");
        }
    }
}
