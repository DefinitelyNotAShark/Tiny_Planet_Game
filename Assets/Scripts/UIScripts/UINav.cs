using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINav : MonoBehaviour
{
    private bool isPaused;

    [SerializeField]
    private GameObject pausePanel;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        isPaused = false;
        Time.timeScale = 1f;//just make sure the time is right on load
    }
    public void OnRestartClicked()
    {
        audio.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void OnMainMenuClicked()
    {
        audio.Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
