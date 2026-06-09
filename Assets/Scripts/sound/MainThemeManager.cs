using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MainThemeManager : MonoBehaviour
{
    public static MainThemeManager instance;

    [Header("FMOD")]
    public EventReference musicEvent;

    private EventInstance musicInstance;
    private bool isPlaying;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.setVolume(0.25f);
        musicInstance.start();

        isPlaying = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wyłącz muzykę w scenie Ending
        if (scene.name == "Ending")
        {
            if (isPlaying)
            {
                musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isPlaying = false;
            }
        }
        // Włącz muzykę po powrocie do MainMenu
        else if (scene.name == "MainMenu")
        {
            if (!isPlaying)
            {
                // Tworzymy nową instancję eventu
                musicInstance.release();

                musicInstance = RuntimeManager.CreateInstance(musicEvent);
                musicInstance.setVolume(0.5f);
                musicInstance.start();

                isPlaying = true;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}