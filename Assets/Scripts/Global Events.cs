using System;
using UnityEngine;

public static class GlobalEvents
{
    public static event Action OnMovementOff;
    public static event Action OnMovementOn;
    public static event Action QTEFailed;

    public static void RaiseOnMovementOff()
    {
        Debug.Log("Raised");
        OnMovementOff?.Invoke();
    }

    public static void RaiseOnMovementOn()
    {
        OnMovementOn?.Invoke();
    }

    public static void RaiseQTEFailed()
    {
        QTEFailed?.Invoke();
    }
}
