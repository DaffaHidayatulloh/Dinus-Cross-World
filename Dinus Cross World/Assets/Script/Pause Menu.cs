using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject surat;
    public GameObject SuratTerbuka;
    public GameObject pauseMenuUI;
    //public Button resumeButton;
    //public Button exitButton;

    private bool isPaused = false;

    void Start()
    {
        // Pastikan menu tidak aktif saat game dimulai
        pauseMenuUI.SetActive(false);

        // Tambahkan listener ke tombol
        //resumeButton.onClick.AddListener(Resume);
        //exitButton.onClick.AddListener(ExitToMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void PauseManual()
    {
        Pause();
    }
    public void Resume()
    {
        surat.SetActive(true);
        SuratTerbuka.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        surat.SetActive(false);
        SuratTerbuka.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); // Pastikan ada scene bernama "MainMenu"
    }
}

