using UnityEngine;
using UnityEngine.UI;

public class KeyPrompt : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptCanvas;
    public Image keyImage;
    public Sprite keyIcon;
    public KeyCode actionKey;

    private bool playerInside;

    private void Start()
    {
        promptCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            keyImage.sprite = keyIcon;
            promptCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            promptCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(actionKey))
        {
            promptCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}