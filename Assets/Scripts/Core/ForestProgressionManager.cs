using UnityEngine;

public class ForestProgressionManager : MonoBehaviour
{
    public EmotionEnvironmentController environmentController;

    public LightBridge bridge;

    private int healedObjects = 0;
    public int requiredHeals = 3;

    public void ObjectHealed()
    {
        healedObjects++;

        environmentController.OnObjectHealed();

        if (healedObjects >= requiredHeals)
        {
            bridge.ActivateBridge();
        }
    }

}