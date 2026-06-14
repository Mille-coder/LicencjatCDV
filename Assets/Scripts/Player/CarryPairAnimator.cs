using UnityEngine;

public class CarryPairAnimator : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator firefighterAnimator;
    [SerializeField] private Animator womanAnimator;

    [Header("State Names")]
    [SerializeField] private string idleHoldingState = "RigFirefighter|IdleHoldingWoman";
    [SerializeField] private string walkHoldingState = "RigFirefighter|WalkWithPersonOnHands";
    [SerializeField] private string putDownState = "RigFirefighter|WomanPutDown";

    public void PlayIdleHolding()
    {
        PlayBoth(idleHoldingState);
    }

    public void PlayWalkHolding()
    {
        PlayBoth(walkHoldingState);
    }

    public void PlayWomanPutDown()
    {
        PlayBoth(putDownState);
    }

    private void PlayBoth(string stateName)
    {
        if (firefighterAnimator != null)
            firefighterAnimator.Play(stateName, 0, 0f);
        else
            Debug.LogWarning("Firefighter Animator is missing.");

        if (womanAnimator != null)
            womanAnimator.Play(stateName, 0, 0f);
        else
            Debug.LogWarning("Woman Animator is missing.");
        
    }

    private void Update()
    {
        
    }
}