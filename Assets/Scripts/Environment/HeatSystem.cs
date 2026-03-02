using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeatSystem : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D playerRb;
    public SpriteRenderer chestGlow;

    public Transform targetPosition;
    public float moveDuration = 2f;
    private bool hasMoved = false;
    
    [Header("Heat Settings")]
    public float speedThreshold = 4f;
    public float heatIncreaseRate = 1f;
    public float heatDecreaseRate = 2f;

    [Range(0,1)]
    public float heatLevel = 0f;

    [Header("Glow Colors")]
    public Color calmColor = new Color(1f, 0.8f, 0.4f);
    public Color angryColor = new Color(1f, 0.2f, 0.1f);
    
    [Header("Camera Shake")]
    public Transform cameraTransform;
    public float shakeAmount = 0.1f;
    private Vector3 originalCamPos;

    [Header("Rumble Audio")]
    public AudioSource rumbleSource;
    public float maxVolume = 0.7f;

    private void Start()
    {
        originalCamPos = cameraTransform.localPosition;
    }

    void Update()
    {
        float speed = Mathf.Abs(playerRb.linearVelocity.x);

        if (speed > speedThreshold)
        {
            heatLevel += Time.deltaTime * heatIncreaseRate;
        }
        else
        {
            heatLevel -= Time.deltaTime * heatDecreaseRate;
        }

        heatLevel = Mathf.Clamp01(heatLevel);
        
        UpdateRumble();
        UpdatePlayerGlow();
        UpdateCameraShake();
    }

    void UpdateRumble()
    {
        if (rumbleSource != null)
        {
            rumbleSource.volume = heatLevel * maxVolume;
        }
    }

    public float GetHeatLevel()
    {
        return heatLevel;
    }

    void UpdateCameraShake()
    {
        if (cameraTransform == null) return;

        if (heatLevel == 1f)
        {
            float shake = shakeAmount * heatLevel;
            cameraTransform.localPosition = originalCamPos + Random.insideUnitSphere * shake;
        }
        else
        {
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                originalCamPos,
                Time.deltaTime * 5f
            );
        }
    }

    void UpdatePlayerGlow()
    {
        if (chestGlow != null)
        {
            chestGlow.color = Color.Lerp(calmColor, angryColor, heatLevel);
        }
    }
    

    [Header("Volcano Progression")]
    public int calmTargetsRequired = 3;
    private int calmCount = 0;

    public GameObject cooledPath;   // exit platform or cooled lava

    public void RegisterCalmEvent()
    {
        calmCount++;

        Debug.Log("Calm event registered. Total: " + calmCount);

        if (calmCount >= calmTargetsRequired)
        {
            TriggerVolcanoCalm();
        }
    }

    void TriggerVolcanoCalm()
    {
        Debug.Log("Volcano calming triggered!");

        if (cooledPath != null)
            cooledPath.SetActive(true);
    }

    IEnumerator MoveObject()
    {
        Vector3 startPosition = playerRb.position;
        Vector3 endPosition = targetPosition.position;

        float timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;
            float t = timer / moveDuration;

            playerRb.position = Vector3.Lerp(startPosition, endPosition, t);
            hasMoved = false;
            yield return null;
        }

        playerRb.position = endPosition;
    }
}