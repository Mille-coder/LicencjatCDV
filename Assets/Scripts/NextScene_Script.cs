using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NextSceneTrigger : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset scene;
#endif

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
            SceneManager.LoadScene(sceneName);
        }
    }
}