using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisabler : MonoBehaviour
{
    [SerializeField] Movement movement;
   

    private void OnEnable()
    {
        GlobalEvents.OnMovementOff += Off;
        GlobalEvents.OnMovementOn += On;
    }

    private void OnDisable()
    {
        GlobalEvents.OnMovementOff -= Off;
        GlobalEvents.OnMovementOn -= On;
    }

    private void Off()
    {
        movement.enabled = false;
    }
    private void On()
    {
        movement.enabled = true;
    }

}
