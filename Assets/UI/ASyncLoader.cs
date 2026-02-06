using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    [Header("Loading Settings")]
    [SerializeField] private float loadingSpeed = 0.5f;
    [SerializeField] private float minLoadTime = 2f;

    [Header("Loading Texts")]
    [SerializeField] private GameObject text0_25;
    [SerializeField] private GameObject text25_50;
    [SerializeField] private GameObject text50_75;
    [SerializeField] private GameObject text75_100;

    public void StartGame(string Tutorial)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        loadingSlider.value = 0f;
        DisableAllTexts();

        StartCoroutine(LoadGameASync(Tutorial));
    }

    IEnumerator LoadGameASync(string Tutorial)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(Tutorial);
        loadOperation.allowSceneActivation = false;

        float timer = 0f;
        float fakeProgress = 0f;

        while (loadOperation.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;

            float realProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            fakeProgress = Mathf.MoveTowards(fakeProgress, realProgress, loadingSpeed * Time.deltaTime);

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
