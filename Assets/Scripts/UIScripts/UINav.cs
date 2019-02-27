using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINav : MonoBehaviour
{
    private bool isPaused;

    [SerializeField]
    private GameObject pausePanel;

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = 1f;//just make sure the time is right on load
    }
    public void OnRestartClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))//if you press the button it toggles pause
        {
            if (!isPaused)
            {
                //Pause
                pausePanel.gameObject.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                //Resume
                pausePanel.gameObject.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
