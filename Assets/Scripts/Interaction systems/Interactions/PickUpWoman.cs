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
        Equipment equipment = interactor.gameObject.GetComponent<Equipment>();
        if (equipment != null)
        {
            equipment.Grabwoman();
        }

        CarryPairAnimator carryPairAnimator = interactor.gameObject.GetComponent<CarryPairAnimator>();

        if (carryPairAnimator == null)
            carryPairAnimator = interactor.gameObject.GetComponentInChildren<CarryPairAnimator>(true);

        if (carryPairAnimator != null)
        {
            carryPairAnimator.StartCarrying();
        }

        OnPickup?.Invoke();

        RuntimeManager.PlayOneShot(pickupSound, transform.position);

        dropoff.SetActive(true);
        gameObject.SetActive(false);

        return true;
    }
}