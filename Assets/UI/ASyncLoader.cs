using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ASyncLoader : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    [Header("Loading Settings")]
    [SerializeField] private float loadingSpeed = 0.5f;
    [SerializeField] private float minLoadTime = 2f;

    [Header("Video")]
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoImage;
    [SerializeField] private RenderTexture renderTexture;

    [Header("Loading Texts")]
    [SerializeField] private GameObject text0_25;
    [SerializeField] private GameObject text25_50;
    [SerializeField] private GameObject text50_75;
    [SerializeField] private GameObject text75_100;

    public void StartGame(string tutorial)
    {
        loadingSlider.value = 0f;
        DisableAllTexts();

        StartCoroutine(BeginLoading(tutorial));
    }

    IEnumerator BeginLoading(string tutorial)
    {
        // Pokazuje panel z filmem
        if (videoPanel != null)
            videoPanel.SetActive(true);

        yield return null;

        // Konfiguracja Render Texture
        if (videoPlayer != null && renderTexture != null)
        {
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = renderTexture;
        }

        // Podłączenie Render Texture do RawImage
        if (videoImage != null && renderTexture != null)
        {
            videoImage.texture = renderTexture;
        }

        // Start filmu
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Prepare();

            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            videoPlayer.Play();

            Debug.Log("Video Started");
        }

        // Start ładowania sceny
        StartCoroutine(LoadGameASync(tutorial));
    }

    IEnumerator LoadGameASync(string tutorial)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(tutorial);
        loadOperation.allowSceneActivation = false;

        float timer = 0f;
        float fakeProgress = 0f;

        while (loadOperation.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;

            float realProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            fakeProgress = Mathf.MoveTowards(
                fakeProgress,
                realProgress,
                loadingSpeed * Time.deltaTime
            );

            loadingSlider.value = fakeProgress;

            UpdateLoadingText(fakeProgress);

            yield return null;
        }

        while (loadingSlider.value < 1f)
        {
            loadingSlider.value += loadingSpeed * Time.deltaTime;

            UpdateLoadingText(loadingSlider.value);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Stop filmu przed zmianą sceny
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        loadOperation.allowSceneActivation = true;
    }

    void DisableAllTexts()
    {
        text0_25.SetActive(false);
        text25_50.SetActive(false);
        text50_75.SetActive(false);
        text75_100.SetActive(false);
    }

    void UpdateLoadingText(float progress)
    {
        DisableAllTexts();

        if (progress < 0.25f)
            text0_25.SetActive(true);

        else if (progress < 0.5f)
            text25_50.SetActive(true);

        else if (progress < 0.75f)
            text50_75.SetActive(true);

        else
            text75_100.SetActive(true);
    }
}