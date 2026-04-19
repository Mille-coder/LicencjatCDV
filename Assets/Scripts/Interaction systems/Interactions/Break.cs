using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour, IInteractable
{
    public bool CanInteract(Interactor interactor)
    {
        if (interactor.gameObject.GetComponent<Equipment>().hasAxe == true)
        {
            Debug.Log("true");
            return true;
        }
        Debug.Log("false");
        return false;

    }

    public bool Interact(Interactor interactor)
    {
        Destroy(gameObject);

        return true;
    }
}
