using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneIntro : MonoBehaviour
{
    public MonoBehaviour playerMovementScript;
    public Text dialogText;
    public string[] dialogLines;
    public string[] postDialogLines;
    public float textDisplayDuration = 3f;
    public float fadeDuration = 0.5f;

    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera targetCamera;
    public float cameraHoldDuration = 3f;
    public int cameraPriorityBoost = 20;

    public float zoomSize = 3f;
    public float zoomDuration = 0.5f;

    private float originalMainZoom;
    private float originalTargetZoom;

    private Color originalColor;
    private int originalMainPriority;
    private int originalTargetPriority;

    public GameObject playerObject;
    public float flipInterval = 0.3f; // waktu antar flip
    public float flipDuration = 2f;

    public Image fadePanel;
    public float fadeInDuration = 1f;

    public GameObject pauseMenuUI;
    public GameObject taskUI;
    public GameObject PauseManager;

    private string cutsceneKey = "Cutscene_Intro";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(cutsceneKey) || PlayerPrefs.GetInt(cutsceneKey) == 0)
        {
            StartCoroutine(PlayCutscene());
        }
        else
        {
            // Pastikan kontrol player aktif dan UI juga muncul langsung
            playerMovementScript.enabled = true;
            pauseMenuUI.SetActive(true);
            taskUI.SetActive(true);
        }
        AudioManager.instance.PlayRainSound(0);
        AudioManager.instance.PlayBGM(1);
        originalColor = dialogText.color;
        originalMainZoom = mainCamera.m_Lens.OrthographicSize;
        originalTargetZoom = targetCamera.m_Lens.OrthographicSize;
    }

    IEnumerator PlayCutscene()
    {
        PauseManager.SetActive(false);
        pauseMenuUI.SetActive(false);
        taskUI.SetActive(false);

        yield return StartCoroutine(FadeInFromBlack());

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Zoom in kedua kamera
        yield return StartCoroutine(ZoomBothCameras(originalMainZoom, zoomSize, zoomDuration));

        StartCoroutine(FlipPlayerBriefly());

        // Dialog pertama
        foreach (string line in dialogLines)
        {
            yield return StartCoroutine(ShowTextWithFade(line));
        }

        // Simpan priority
        originalMainPriority = mainCamera.Priority;
        originalTargetPriority = targetCamera.Priority;

        // Pindah ke kamera cutscene
        targetCamera.Priority += cameraPriorityBoost;

        yield return new WaitForSeconds(cameraHoldDuration);

        // Kembali ke kamera utama
        targetCamera.Priority = originalTargetPriority;
        mainCamera.Priority = originalMainPriority + cameraPriorityBoost;

        yield return new WaitForSeconds(0.5f);

        // Dialog kedua
        foreach (string line in postDialogLines)
        {
            yield return StartCoroutine(ShowTextWithFade(line));
        }

        // Zoom out kedua kamera
        yield return StartCoroutine(ZoomBothCameras(zoomSize, originalMainZoom, zoomDuration));

        pauseMenuUI.SetActive(true);
        taskUI.SetActive(true);
        PauseManager.SetActive(true);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        PlayerPrefs.SetInt(cutsceneKey, 1);
        PlayerPrefs.Save();
    }

    IEnumerator ShowTextWithFade(string line)
    {
        dialogText.text = line;
        float t = 0f;

        // Fade In
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            dialogText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(textDisplayDuration);

        // Fade Out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (t / fadeDuration));
            dialogText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        dialogText.text = "";
    }

    IEnumerator ZoomBothCameras(float fromSize, float toSize, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float size = Mathf.Lerp(fromSize, toSize, t / duration);
            mainCamera.m_Lens.OrthographicSize = size;
            targetCamera.m_Lens.OrthographicSize = size;
            yield return null;
        }
        mainCamera.m_Lens.OrthographicSize = toSize;
        targetCamera.m_Lens.OrthographicSize = toSize;
    }
    IEnumerator FlipPlayerBriefly()
    {
        float timer = 0f;
        bool flipped = false;
        Vector3 originalScale = playerObject.transform.localScale;

        while (timer < flipDuration)
        {
            flipped = !flipped;
            Vector3 scale = originalScale;
            scale.x *= flipped ? -1 : 1;
            playerObject.transform.localScale = scale;

            yield return new WaitForSeconds(flipInterval);
            timer += flipInterval;
        }

        // Pastikan kembali ke arah asli
        playerObject.transform.localScale = originalScale;
    }
    IEnumerator FadeInFromBlack()
    {
        fadePanel.gameObject.SetActive(true);
        Color color = fadePanel.color;
        color.a = 1f;
        fadePanel.color = color;

        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeInDuration);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;
        fadePanel.gameObject.SetActive(false);
    }

}

