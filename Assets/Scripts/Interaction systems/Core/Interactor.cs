using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float castDistance = 5f;
    [SerializeField] private Vector3 raycastOffset = new Vector3(0, 1f, 0);

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(InteractionTest(out IInteractable interactable))
            {
                Debug.Log("Interacted");
                interactable.Interact(this);
            }
        }
    }
    private bool InteractionTest(out IInteractable interactable)
    {
        interactable = null;
        Ray ray = new Ray(transform.position + raycastOffset, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, castDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();

            if( interactable != null)
            {
                if(interactable.CanInteract(this))
                {
                    return true;
                }
                
            }

            return false;
        }

        return false;
    }

}
