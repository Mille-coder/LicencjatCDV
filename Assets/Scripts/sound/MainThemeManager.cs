using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainThemeManager : MonoBehaviour
{
    public static MainThemeManager instance;

    [EventRef]
    public string musicEvent;

    private EventInstance musicInstance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    void OnDestroy()
    {
        // Sprawdzenie czy instancja istnieje zanim ją zatrzymamy
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}