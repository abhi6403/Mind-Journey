using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    public void MoveLeft(bool state)
    {
        horizontal = state ? -1 : 0;
    }

    public void MoveRight(bool state)
    {
        horizontal = state ? 1 : 0;
    }

    public void MoveUp(bool state)
    {
        vertical = state ? 1 : 0;
    }

    public void MoveDown(bool state)
    {
        vertical = state ? -1 : 0;
    }
}