using UnityEngine;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    [Header("Object To Activate")]
    public GameObject objectToActivate;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                objectToActivate.GetComponent<UIFadeOut>().StartFadeOut();
            }
        }
    }
}