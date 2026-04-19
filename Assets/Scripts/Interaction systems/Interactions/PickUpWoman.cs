using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class PickUpWoman : MonoBehaviour, IInteractable
{
    public static event Action OnPickup;
    [SerializeField] GameObject dropoff;

    public EventReference pickupSound;

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        interactor.gameObject.GetComponent<Equipment>().Grabwoman();

        OnPickup?.Invoke();

        RuntimeManager.PlayOneShot(pickupSound, transform.position); // 🔥 FMOD
        dropoff.SetActive(true);
        gameObject.SetActive(false);
        return true;
    }
}