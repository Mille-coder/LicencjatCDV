using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenDoor :MonoBehaviour, IInteractable
{

    [SerializeField] Transform door;
    [SerializeField] BoxCollider doorcollider;
    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        door.Rotate(0,90,0);
        doorcollider.enabled = false;
        return true;
        
    }


}
