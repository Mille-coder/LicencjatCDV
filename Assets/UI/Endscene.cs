using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;
using System.Collections;

public class Endscene : MonoBehaviour
{
    public GameObject mainMenu;      // Panel lub przycisk menu
    public VideoPlayer videoPlayer;  // Komponent VideoPlayer

    [Header("Typing Text")]
    public GameObject textPanel;     // Panel z tekstem ze sceny
    public TMP_Text textField;       // Komponent TextMeshPro
    [TextArea]
    public string message;           // Tekst do wyświetlenia

    public float delayBeforeText = 20f; // Opóźnienie w sekundach
    public float letterDelay = 0.05f;   // Czas między literami

    private void Start()
    {
        // Ukryj przycisk/menu na początku
        mainMenu.SetActive(false);

        // Ukryj panel tekstu
        textPanel.SetActive(false);

        // Odtwórz film
        videoPlayer.Play();

        // Nasłuchiwanie zakończenia filmu
        videoPlayer.loopPointReached += OnVideoFinished;

        // Uruchom wyświetlanie tekstu po 20 sekundach
        StartCoroutine(ShowTextAfterDelay());
    }

    private IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeText);

        textPanel.SetActive(true);
        textField.text = "";

        foreach (char letter in message)
        {
            textField.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Pokaż przycisk/menu po zakończeniu filmu
        mainMenu.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

