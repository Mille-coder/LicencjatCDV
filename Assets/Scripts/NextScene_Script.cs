using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NextSceneTrigger : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset scene;
#endif

    [Header("Event przed zmian¹ sceny")]
    public UnityEvent onBeforeSceneLoad;

    private string sceneName;

    private void Awake()
    {
#if UNITY_EDITOR
        if (scene != null)
        {
            sceneName = scene.name;
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onBeforeSceneLoad?.Invoke();

            SceneManager.LoadScene(sceneName);
        }
    }
}