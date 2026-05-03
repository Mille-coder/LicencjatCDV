using UnityEngine;

public class AttachQuadToCanvas : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private Vector3 localPosition = Vector3.zero;
    [SerializeField] private Vector3 localRotation = Vector3.zero;
    [SerializeField] private Vector3 localScale = Vector3.one;

    private void Start()
    {
        if (targetCanvas == null)
        {
            Debug.LogWarning("Brak przypisanego Canvas.");
            return;
        }

        if (targetCanvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogWarning("Canvas musi mieć Render Mode = World Space.");
            return;
        }

        transform.SetParent(targetCanvas.transform, false);

        transform.localPosition = localPosition;
        transform.localEulerAngles = localRotation;
        transform.localScale = localScale;
    }
}