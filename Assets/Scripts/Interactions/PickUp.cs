using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        interactor.gameObject.GetComponent<Equipment>().Grabaxe();
        gameObject.SetActive(false);
        return true;
    }
}
