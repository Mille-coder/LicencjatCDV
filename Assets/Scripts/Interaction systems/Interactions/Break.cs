using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour, IInteractable
{
    [SerializeField] private string axeSwingTrigger = "AxeSwing";
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
        Animator animator = interactor.gameObject.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(axeSwingTrigger);
        }
        else
        {
            Debug.LogWarning("Break interaction could not find an Animator on the player or its children.");
        }
        Destroy(gameObject);

        return true;
    }
}
