using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLight : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Final light reached.");

            SceneManager.LoadScene("Ending");
        }
    }
}