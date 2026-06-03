using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropoff : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Woman1;
    [SerializeField] GameObject Woman2;
    [SerializeField] GameObject CarriedWoman;

    [Header("Event po od³o¿eniu kobiety")]
    public UnityEvent onDropoff;

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        if (Woman1.activeSelf)
        {
            Woman2.SetActive(true);
            CarriedWoman.SetActive(false);

            onDropoff?.Invoke();

            gameObject.SetActive(false);
            return true;
        }

        Woman1.SetActive(true);
        CarriedWoman.SetActive(false);

        onDropoff?.Invoke();

        gameObject.SetActive(false);
        return true;
    }
}