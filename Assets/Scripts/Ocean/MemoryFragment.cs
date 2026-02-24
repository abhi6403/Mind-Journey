using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public OceanMovementModifier oceanManager;
    public OceanProgression oceanProgression;

    [Header("Follow Settings")]
    public float followSpeed = 3f;
    public Vector3 followOffset = new Vector3(0, 1f, 0);

    [Header("Release Settings")]
    public float releaseFloatSpeed = 2f;
    public float destroyAfterSeconds = 2f;

    private bool collected = false;
    private bool canRelease = false;
    private bool released = false;

    void Update()
    {
        // FOLLOW PLAYER
        if (collected && !released)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                player.position + followOffset,
                Time.deltaTime * followSpeed
            );
        }

        // RELEASE INPUT
        if (canRelease && collected && Input.GetKeyDown(KeyCode.R))
        {
            ReleaseFragment();
        }

        // FLOAT UP AFTER RELEASE
        if (released)
        {
            transform.position += Vector3.up * releaseFloatSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;

            if (oceanManager != null)
                oceanManager.AddFragmentWeight();

            Debug.Log("Memory Fragment Collected");
        }

        if (other.CompareTag("Surface"))
        {
            canRelease = true;
            Debug.Log("At Surface - Press R to Release");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
            canRelease = false;
        }
    }

    void ReleaseFragment()
    {
        released = true;
        collected = false;

        if (oceanManager != null)
            oceanManager.RemoveFragmentWeight();

        if (oceanProgression != null)
            oceanProgression.RegisterRelease();

        Debug.Log("Memory Released");

        Destroy(gameObject, destroyAfterSeconds);
    }
}