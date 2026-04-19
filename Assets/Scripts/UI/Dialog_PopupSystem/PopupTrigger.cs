using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    private PopupManager popupManager;

    private void Start()
    {
        popupManager = FindObjectOfType<PopupManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        popupManager.ShowPopup(GetComponent<Collider>());
    }
}
