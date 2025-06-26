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

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
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

    void Pause()
    {
        AudioManager.instance.PlaySFX(13);
        surat.SetActive(false);
        SuratTerbuka.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        AudioManager.instance.PauseAllAudio(); // pause, bukan stop
        AudioManager.instance.StopWalkSound();

    }

    public void Resume()
    {
        AudioManager.instance.PlaySFX(16);
        surat.SetActive(true);
        SuratTerbuka.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        AudioManager.instance.ResumeAllAudio(); // lanjutkan
    }


    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.instance.StopBGM();
        AudioManager.instance.StopWalkSound();
        AudioManager.instance.StopRainSound();
        SceneManager.LoadScene("Main Menu");
    }
}


