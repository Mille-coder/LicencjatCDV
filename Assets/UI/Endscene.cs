using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Endscene : MonoBehaviour
{
    public GameObject mainMenu;      // Panel lub przycisk menu
    public VideoPlayer videoPlayer;  // Komponent VideoPlayer

    private void Start()
    {
        // Ukryj przycisk/menu na początku
        mainMenu.SetActive(false);

        // Odtwórz film
        videoPlayer.Play();

        // Nasłuchiwanie zakończenia filmu
        videoPlayer.loopPointReached += OnVideoFinished;
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