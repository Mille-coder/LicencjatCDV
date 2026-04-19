using UnityEngine;
using UnityEngine.UI;

public class KeyPrompt : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptCanvas;
    public Image keyImage;
    public Sprite keyIcon;

    private void Start()
    {
        promptCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyImage.sprite = keyIcon;   
            promptCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptCanvas.SetActive(false);
        }
    }
}