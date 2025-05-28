using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneIntro : MonoBehaviour
{
    public MonoBehaviour playerMovementScript;
    public Text dialogText;
    public string[] dialogLines;        // dialog sebelum kamera berpindah
    public string[] postDialogLines;    // dialog setelah kamera kembali
    public float textDisplayDuration = 3f;
    public float fadeDuration = 0.5f;

    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera targetCamera;
    public float cameraHoldDuration = 3f;
    public int cameraPriorityBoost = 20;

    private Color originalColor;
    private int originalMainPriority;
    private int originalTargetPriority;

    private void Start()
    {
        originalColor = dialogText.color;
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

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

        yield return new WaitForSeconds(0.5f); // jeda transisi balik

        // Dialog kedua
        foreach (string line in postDialogLines)
        {
            yield return StartCoroutine(ShowTextWithFade(line));
        }

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
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
}
