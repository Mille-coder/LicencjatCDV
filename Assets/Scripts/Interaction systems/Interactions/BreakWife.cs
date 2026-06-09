using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWife : MonoBehaviour, IInteractable
{
    [SerializeField] private string axeSwingTrigger = "AxeSwing";
    public bool CanInteract(Interactor interactor)
    {
        Debug.Log("true");
        return true;
        

    }

    public bool Interact(Interactor interactor)
    {
        
        Destroy(gameObject);

        return true;
    }
}
