using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;
using FMOD.Studio;

public class OpenDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Transform door;
    [SerializeField] BoxCollider doorcollider;

    // 🎧 FMOD event
    [SerializeField] private EventReference doorOpenEvent;

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        // 🚪 animacja drzwi
        door.Rotate(0, 90, 0);
        doorcollider.enabled = false;

        // 🎧 dźwięk otwierania drzwi
        RuntimeManager.PlayOneShot(doorOpenEvent, transform.position);

        return true;
    }
}