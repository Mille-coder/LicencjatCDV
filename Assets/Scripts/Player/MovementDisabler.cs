using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisabler : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Interactor interactor;
   

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
        interactor.enabled = false;
        
    }
    private void On()
    {
        movement.enabled = true;
        interactor.enabled = true;
    }

}
