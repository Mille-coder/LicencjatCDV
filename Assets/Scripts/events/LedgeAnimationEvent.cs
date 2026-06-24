using UnityEngine;

public class LedgeAnimationEvents : MonoBehaviour
{
    [SerializeField] private Movement movement;

    public void FinishLedgeClimb()
    {
        if (movement != null)
        {
            movement.FinishLedgeClimb();
        }
    }
}