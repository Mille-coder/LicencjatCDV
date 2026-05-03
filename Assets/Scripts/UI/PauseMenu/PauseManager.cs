using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public string MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}
