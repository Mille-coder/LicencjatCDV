using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject OptionsMenu;
    public GameObject mainMenu;

    [SerializeField] private ASyncLoader asyncLoader;
    [SerializeField] private string tutorialScene = "Tutorial";

    public void NewGame()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        asyncLoader.StartGame(tutorialScene);

        Debug.Log("New Game Clicked");

        GameManager.StartedFromMenu = true;

        SceneManager.LoadScene("Tutorial");
    }

    public void Options()
    {
        OptionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}