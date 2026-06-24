using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dropoff : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Woman1;
    [SerializeField] private GameObject Woman2;
    [SerializeField] private GameObject CarriedWoman;

    [SerializeField] private Equipment equipment;

    [Header("Prompt UI")]
    [SerializeField] private GameObject promptCanvas;
    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite eIcon;

    [Header("Dropoff Animation")]
    [SerializeField] private float putDownHideDelay = 0.8f;

    [Header("Event po odłożeniu kobiety")]
    public UnityEvent onDropoff;

    private bool isDroppingOff;

    private void Start()
    {
        if (promptCanvas != null)
            promptCanvas.SetActive(false);
    }

    public bool CanInteract(Interactor interactor)
    {
        Equipment currentEquipment = equipment;

        if (currentEquipment == null && interactor != null)
            currentEquipment = interactor.gameObject.GetComponent<Equipment>();

        bool canInteract = currentEquipment != null && currentEquipment.haswoman && !isDroppingOff;

        if (promptCanvas != null)
            promptCanvas.SetActive(canInteract);

        if (canInteract && keyImage != null)
            keyImage.sprite = eIcon;

        return canInteract;
    }

    public bool Interact(Interactor interactor)
    {
        Equipment currentEquipment = equipment;

        if (currentEquipment == null && interactor != null)
            currentEquipment = interactor.gameObject.GetComponent<Equipment>();

        if (currentEquipment == null || !currentEquipment.haswoman || isDroppingOff)
            return false;

        if (promptCanvas != null)
            promptCanvas.SetActive(false);

        CarryPairAnimator carryPairAnimator = null;

        if (interactor != null)
        {
            carryPairAnimator = interactor.gameObject.GetComponent<CarryPairAnimator>();

            if (carryPairAnimator == null)
                carryPairAnimator = interactor.gameObject.GetComponentInChildren<CarryPairAnimator>(true);
        }

        if (carryPairAnimator != null)
        {
            carryPairAnimator.StopCarrying();
        }

        StartCoroutine(DropoffRoutine(currentEquipment, carryPairAnimator));

        return true;
    }

    private IEnumerator DropoffRoutine(Equipment currentEquipment, CarryPairAnimator carryPairAnimator)
    {
        isDroppingOff = true;

        yield return new WaitForSeconds(putDownHideDelay);

        if (Woman1 != null && Woman1.activeSelf)
        {
            if (Woman2 != null)
                Woman2.SetActive(true);
        }
        else
        {
            if (Woman1 != null)
                Woman1.SetActive(true);
        }

        if (carryPairAnimator != null)
            carryPairAnimator.HideCarriedWoman();

        if (currentEquipment != null)
            currentEquipment.Dropwoman();

        if (CarriedWoman != null)
            CarriedWoman.SetActive(false);

        onDropoff?.Invoke();

        gameObject.SetActive(false);

        isDroppingOff = false;
    }
}