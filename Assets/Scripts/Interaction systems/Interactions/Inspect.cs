using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inspect : MonoBehaviour, IInteractable
{
    bool interacted = false;
    [SerializeField] private GameObject image;
    public bool CanInteract(Interactor interactor)
    {
        if (!interacted)
        {
            return true;
        }
        return false;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Trying");
        image.gameObject.SetActive(true);
        GlobalEvents.RaiseOnMovementOff();
        return true;
    }

    
}
