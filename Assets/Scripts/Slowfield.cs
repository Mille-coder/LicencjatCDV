using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowfield : MonoBehaviour
{
    void OnTriggerEnter()
    {
        GlobalEvents.RaiseOnSlowOn();
        Debug.Log("Collided");
    }

    void OnTriggerExit()
    {
        GlobalEvents.RaiseOnSlowOff();
    }
}
