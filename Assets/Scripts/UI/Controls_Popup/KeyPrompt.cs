using UnityEngine;
using UnityEngine.UI;

public class KeyPrompt : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptCanvas;
    public Image keyImage;
    public Sprite keyIcon;
    public KeyCode actionKey;

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

    public void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            promptCanvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}