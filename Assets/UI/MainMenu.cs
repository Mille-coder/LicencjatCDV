using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        Debug.Log("New Game Clicked");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
