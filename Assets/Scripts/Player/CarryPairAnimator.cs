using UnityEngine;

public class CarryPairAnimator : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator firefighterAnimator;
    [SerializeField] private Animator womanAnimator;

    [Header("Woman Model")]
    [SerializeField] private GameObject womanModel;

    [Header("Firefighter State Names")]
    [SerializeField] private string firefighterIdleHoldingState = "RigFirefighter|IdleHoldingWoman";
    [SerializeField] private string firefighterWalkHoldingState = "RigFirefighter|WalkWithPersonOnHands";
    [SerializeField] private string firefighterPutDownState = "RigFirefighter|WomanPutDown";

    [Header("Woman State Names")]
    [SerializeField] private string womanIdleHoldingState = "RigWoman1|IdleHoldingWoman";
    [SerializeField] private string womanWalkHoldingState = "RigWoman1|WalkWithPersonOnHands";
    [SerializeField] private string womanPutDownState = "RigWoman1|WomanPutDown";

    [Header("Jump With Woman")]
    [SerializeField] private string jumpWithWomanTrigger = "JumpWithWoman";
    [SerializeField] private bool womanUsesJumpTrigger = true;
    [SerializeField] private string womanJumpWithWomanState = "RigWoman1|JumpWithWoman";

    private bool isCarrying;
    private bool isCarryWalking;

    public void StartCarrying()
    {
        isCarrying = true;
        isCarryWalking = false;

        if (womanModel != null)
            womanModel.SetActive(true);

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isCarryingWoman", true);
            firefighterAnimator.SetBool("WomanCarryIdle", true);
            firefighterAnimator.SetBool("WomanCarryWalk", false);
            firefighterAnimator.Play(firefighterIdleHoldingState, 0, 0f);
        }

        if (womanAnimator != null)
        {
            womanAnimator.SetBool("WomanCarryIdle", true);
            womanAnimator.SetBool("WomanCarryWalk", false);
            womanAnimator.Play(womanIdleHoldingState, 0, 0f);
        }
    }

    public void StopCarrying()
    {
        isCarrying = false;
        isCarryWalking = false;

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isCarryingWoman", false);
            firefighterAnimator.SetBool("WomanCarryIdle", false);
            firefighterAnimator.SetBool("WomanCarryWalk", false);
            firefighterAnimator.Play(firefighterPutDownState, 0, 0f);
        }

        if (womanAnimator != null)
        {
            womanAnimator.SetBool("WomanCarryIdle", false);
            womanAnimator.SetBool("WomanCarryWalk", false);
            womanAnimator.Play(womanPutDownState, 0, 0f);
        }
    }

    public void HideCarriedWoman()
    {
        if (womanModel != null)
            womanModel.SetActive(false);
    }

    public void SetCarryWalking(bool isWalking)
    {
        if (!isCarrying)
            return;

        if (isCarryWalking == isWalking)
            return;

        isCarryWalking = isWalking;

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isCarryingWoman", true);
            firefighterAnimator.SetBool("WomanCarryIdle", !isWalking);
            firefighterAnimator.SetBool("WomanCarryWalk", isWalking);
            firefighterAnimator.SetBool("isRunning", isWalking);

            if (isWalking)
                firefighterAnimator.Play(firefighterWalkHoldingState, 0, 0f);
            else
                firefighterAnimator.Play(firefighterIdleHoldingState, 0, 0f);
        }

        if (womanAnimator != null)
        {
            womanAnimator.SetBool("WomanCarryIdle", !isWalking);
            womanAnimator.SetBool("WomanCarryWalk", isWalking);

            if (isWalking)
                womanAnimator.Play(womanWalkHoldingState, 0, 0f);
            else
                womanAnimator.Play(womanIdleHoldingState, 0, 0f);
        }
    }

    public void PlayJumpWithWoman()
    {
        if (!isCarrying)
            return;

        isCarryWalking = false;

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isCarryingWoman", true);
            firefighterAnimator.SetBool("WomanCarryIdle", true);
            firefighterAnimator.SetBool("WomanCarryWalk", false);
            firefighterAnimator.SetTrigger(jumpWithWomanTrigger);
        }

        if (womanAnimator != null)
        {
            womanAnimator.SetBool("WomanCarryIdle", true);
            womanAnimator.SetBool("WomanCarryWalk", false);

            if (womanUsesJumpTrigger)
            {
                womanAnimator.SetTrigger(jumpWithWomanTrigger);
            }
            else if (!string.IsNullOrWhiteSpace(womanJumpWithWomanState))
            {
                womanAnimator.Play(womanJumpWithWomanState, 0, 0f);
            }
        }
    }
}