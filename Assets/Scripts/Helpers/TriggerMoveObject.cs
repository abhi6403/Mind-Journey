using UnityEngine;
using System.Collections;

public class TriggerMoveObject : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform objectToMove;
    public Transform targetPosition;
    public float moveDuration = 2f;

    private bool hasMoved = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasMoved && other.CompareTag("Player"))
        {
            StartCoroutine(MoveObject());
            hasMoved = true;
        }
    }

    IEnumerator MoveObject()
    {
        Vector3 startPosition = objectToMove.position;
        Vector3 endPosition = targetPosition.position;

        float timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;
            float t = timer / moveDuration;

            objectToMove.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        objectToMove.position = endPosition;
    }
}