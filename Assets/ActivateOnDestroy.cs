using UnityEngine;

public class ActivateOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject replacementObject;

    private bool activated = false;

    private void Update()
    {
        if (!activated && targetObject == null)
        {
            ActivateReplacement();
        }
    }

    private void ActivateReplacement()
    {
        if (replacementObject != null)
        {
            replacementObject.SetActive(true);
        }

        activated = true;
    }
}