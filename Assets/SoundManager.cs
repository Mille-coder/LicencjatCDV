using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DebugVolumeController : MonoBehaviour
{
    [Header("Debug")]
    public bool enableDebugVolume = true;

    [Range(0f, 1f)]
    public float startVolume = 0.75f;

    public float volumeStep = 0.05f;

    private Bus masterBus;
    private float currentVolume;

    void Start()
    {
        // Jeśli weszliśmy z Main Menu -> wyłącz debug
        if (GameManager.StartedFromMenu)
        {
            enableDebugVolume = false;
            return;
        }

        masterBus = RuntimeManager.GetBus("bus:/");

        currentVolume = startVolume;
        masterBus.setVolume(currentVolume);

        Debug.Log("DEBUG AUDIO ENABLED");
    }

    void Update()
    {
        if (!enableDebugVolume)
            return;

        // Głośniej
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            currentVolume += volumeStep;
            currentVolume = Mathf.Clamp01(currentVolume);

            masterBus.setVolume(currentVolume);

            Debug.Log("Volume: " + currentVolume);
        }

        // Ciszej
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            currentVolume -= volumeStep;
            currentVolume = Mathf.Clamp01(currentVolume);

            masterBus.setVolume(currentVolume);

            Debug.Log("Volume: " + currentVolume);
        }
    }
}