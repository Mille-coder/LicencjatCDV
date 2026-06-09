using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class NextSceneTrigger : MonoBehaviour
{
    [Header("Event przed zmianą sceny")]
    public UnityEvent onBeforeSceneLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onBeforeSceneLoad?.Invoke();

            SceneManager.LoadScene("Ending");
        }
    }
}