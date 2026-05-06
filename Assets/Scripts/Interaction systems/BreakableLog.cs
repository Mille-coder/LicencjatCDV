using System.Collections;
using UnityEngine;

public class BreakableLog : MonoBehaviour
{
    [SerializeField] private GameObject replacementPrefab;

    public bool CanInteract(Interactor interactor)
    {
        Equipment equipment = interactor.GetComponent<Equipment>();
        return equipment != null && equipment.hasAxe;
    }

    public bool Interact(Interactor interactor)
    {
        if (replacementPrefab != null)
        {
            Instantiate(
                replacementPrefab,
                transform.position,
                transform.rotation
            );
        }

        Destroy(gameObject);
        return true;
    }
}