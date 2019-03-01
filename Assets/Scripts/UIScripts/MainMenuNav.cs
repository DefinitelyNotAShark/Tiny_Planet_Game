using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuNav : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonClick;

    [SerializeField]
    private float  buttonClickVolume;

    [SerializeField]
    private GameObject startPanel, instructionsPanel;

    private AudioSource audioSource;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartPressed()
    {
        audioSource.PlayOneShot(buttonClick, buttonClickVolume);
        startPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    public void OnButtonPressed()
    {
        audioSource.PlayOneShot(buttonClick, buttonClickVolume);
    }

    public void OnHowToStartPressed()
    {
        audioSource.PlayOneShot(buttonClick, buttonClickVolume);
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
