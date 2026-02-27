using UnityEngine;

public class ForestProgressionManager : MonoBehaviour
{
    [Header("References")]
    public EmotionEnvironmentController environmentController;
    public LightBridge bridge;

    [Header("Progress Settings")]
    public int requiredHeals = 10;   // How many needed to activate bridge

    private int healedObjects = 0;
    private bool bridgeActivated = false;

    public void ObjectHealed()
    {
        healedObjects++;

        Debug.Log("Total Healed Objects: " + healedObjects);

        if (environmentController != null)
        {
            environmentController.OnObjectHealed(healedObjects);
        }

        if (!bridgeActivated && healedObjects >= requiredHeals)
        {
            bridgeActivated = true;

            if (bridge != null)
                bridge.ActivateBridge();
        }
    }
}