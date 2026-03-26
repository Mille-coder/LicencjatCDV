using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float castDistance = 5f;
    [SerializeField] private Vector3 raycastOffset = new Vector3(0, 1f, 0);

    private Renderer[] currentRenderers;
    private IInteractable currentInteractable;

    private void Update()
    {
        if (InteractionTest(out IInteractable interactable))
        {
            Highlight(interactable);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacted");
                interactable.Interact(this);
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private bool InteractionTest(out IInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(transform.position + raycastOffset, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, castDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                return interactable.CanInteract(this);
            }

            return false;
        }

        return false;
    }

    private void Highlight(IInteractable interactable)
    {
        if (interactable == currentInteractable) return;

        ClearHighlight();

        currentInteractable = interactable;

        GameObject obj = ((MonoBehaviour)interactable).gameObject;
        currentRenderers = obj.GetComponentsInChildren<Renderer>();

        foreach (var r in currentRenderers)
        {
            if (r.gameObject != obj)
                r.material.SetFloat("_CanPickup", 1f);
        }
    }

    private void ClearHighlight()
    {
        if (currentRenderers == null) return;

        foreach (var r in currentRenderers)
        {
            if (r != null)
                r.material.SetFloat("_CanPickup", 0f);
        }

        currentRenderers = null;
        currentInteractable = null;
    }
}