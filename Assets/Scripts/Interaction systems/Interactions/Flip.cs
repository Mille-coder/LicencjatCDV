using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour, IInteractable
{
    [SerializeField] Animator animator;
    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        animator.SetTrigger("Flip");
        return true;
    }


}
