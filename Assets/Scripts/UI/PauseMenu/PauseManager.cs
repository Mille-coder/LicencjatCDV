using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public string MainMenu;

    [Header("Pause Quad")]
    public Transform pauseQuad;
    public Transform playerCamera;
    public float quadDistance = 2f;
    public Vector3 quadOffset = Vector3.zero;

    void Start()
    {
        pauseCanvas.SetActive(false);

        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseCanvas.activeSelf)
            {
                ShowPause();
            }
            else
            {
                Continue();
            }
        }
    }

    void ShowPause()
    {
        if (pauseQuad != null && playerCamera != null)
        {
            pauseQuad.position =
                playerCamera.position +
                playerCamera.forward * quadDistance +
                playerCamera.right * quadOffset.x +
                playerCamera.up * quadOffset.y;

            pauseQuad.rotation = playerCamera.rotation;
        }

       
        EventSystem.current.SetSelectedGameObject(null);

        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        
        EventSystem.current.SetSelectedGameObject(null);

        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}