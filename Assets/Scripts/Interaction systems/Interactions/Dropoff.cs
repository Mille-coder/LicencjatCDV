using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dropoff : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Woman1;
    [SerializeField] GameObject Woman2;
    [SerializeField] GameObject CarriedWoman;

    [SerializeField] private Equipment equipment;

    [Header("Prompt UI")]
    [SerializeField] private GameObject promptCanvas;
    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite eIcon;

    [Header("Event po odłożeniu kobiety")]
    public UnityEvent onDropoff;

    private void Start()
    {
        if (promptCanvas != null)
            promptCanvas.SetActive(false);
    }

    public bool CanInteract(Interactor interactor)
    {
        bool canInteract = equipment != null && equipment.haswoman;

        if (promptCanvas != null)
            promptCanvas.SetActive(canInteract);

        if (canInteract && keyImage != null)
            keyImage.sprite = eIcon;

        return canInteract;
    }

    public bool Interact(Interactor interactor)
    {
        if (equipment == null || !equipment.haswoman)
            return false;

        if (promptCanvas != null)
            promptCanvas.SetActive(false);

        if (Woman1.activeSelf)
        {
            Woman2.SetActive(true);
            CarriedWoman.SetActive(false);
        }
        else
        {
            Woman1.SetActive(true);
            CarriedWoman.SetActive(false);
        }

        equipment.haswoman = false;

        onDropoff?.Invoke();

        gameObject.SetActive(false);

        return true;
    }
}