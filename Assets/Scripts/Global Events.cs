using System;
using UnityEngine;

public static class GlobalEvents
{
    public static event Action OnMovementOff;
    public static event Action OnMovementOn;
    public static event Action OnSlowOn;
    public static event Action OnSlowOff;
    public static event Action OnPlayerDeath;

    public static void RaiseOnMovementOff()
    {
        Debug.Log("Raised");
        OnMovementOff?.Invoke();
    }

    public static void RaiseOnMovementOn()
    {
        OnMovementOn?.Invoke();
    }

    public static void RaiseOnSlowOn()
    {
        OnSlowOn?.Invoke();
    }

    public static void RaiseOnSlowOff()
    {
        OnSlowOff?.Invoke();
    }

    public static void RaiseOnPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    
}
