using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainThemeManager : MonoBehaviour
{
    public static MainThemeManager instance;

    public EventReference musicEvent;

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

        musicInstance.setVolume(0.5f);

        musicInstance.start();
    }

    void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}