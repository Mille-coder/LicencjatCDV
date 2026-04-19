using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class PickUp : MonoBehaviour, IInteractable
{
    public static event Action OnPickup;

    public EventReference pickupSound;

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        interactor.gameObject.GetComponent<Equipment>().Grabaxe();

        OnPickup?.Invoke();

        RuntimeManager.PlayOneShot(pickupSound, transform.position); // 🔥 FMOD

        gameObject.SetActive(false);
        return true;
    }
}