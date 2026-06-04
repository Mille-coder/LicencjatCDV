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
    [SerializeField] private string pickUpTrigger = "PickUp";

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Animator animator = interactor.gameObject.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(pickUpTrigger);
        }
        else
        {
            Debug.LogWarning("PickUp interaction could not find an Animator on the player or its children.");
        }
        
        interactor.gameObject.GetComponent<Equipment>().Grabaxe();

        OnPickup?.Invoke();

        RuntimeManager.PlayOneShot(pickupSound, transform.position); // 🔥 FMOD

        gameObject.SetActive(false);
        return true;
    }
}