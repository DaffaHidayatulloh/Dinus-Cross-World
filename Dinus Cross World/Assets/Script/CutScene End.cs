using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneEnd : MonoBehaviour
{
    public TaskManager taskManager;
    public GameObject taskUI;
    public GameObject player;
    public Image fadeImage; // UI Image yang akan di-fade in
    public GameObject npcPakHamadi; // Referensi ke GameObject Pak Hamadi

    [Header("Got Item UI")]
    public GameObject gotItemUI;
    public GotItem gotItemScript;

    [Header("Pause System")]
    public GameObject pauseMenuUI; // UI pause menu
    public MonoBehaviour pauseScript; // script pause, misal PauseMenu.cs

    [Header("Settings")]
    public float fadeDuration = 2f;

    private PlayerMovement playerMovement;
    private bool hasTriggered = false;

    void Start()
    {
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!hasTriggered && taskManager != null && taskManager.GetCurrentTaskIndex() == 8)
        {
            TriggerCutscene();
            hasTriggered = true;
        }
    }

    void TriggerCutscene()
    {
        if (taskUI != null) taskUI.SetActive(false);
        if (playerMovement != null) playerMovement.enabled = false;

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (pauseScript != null) pauseScript.enabled = false;

        StartCoroutine(ShowGotItemThenFade());
    }

    IEnumerator ShowGotItemThenFade()
    {
        if (gotItemScript != null && gotItemUI != null)
        {
            gotItemScript.isClosed = false;     // Reset status tutup
            gotItemUI.SetActive(true);

            yield return new WaitUntil(() => gotItemScript.isClosed);
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeInImage());
            StartCoroutine(FlipPlayerRepeatedly());
        }

        if (npcPakHamadi != null)
        {
            SpriteRenderer sr = npcPakHamadi.GetComponent<SpriteRenderer>();
            if (sr != null)
                StartCoroutine(FadeOutNPC(sr));
            else
                npcPakHamadi.SetActive(false);
        }
    }

    IEnumerator FadeInImage()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        //StartCoroutine(FlipPlayerRepeatedly());
    }

    IEnumerator FadeOutNPC(SpriteRenderer sr)
    {
        float elapsed = 0f;
        Color c = sr.color;
        c.a = 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            c.a = alpha;
            sr.color = c;
            yield return null;
        }

        npcPakHamadi.SetActive(false); // Setelah fade out, nonaktifkan objek
    }

    IEnumerator FlipPlayerRepeatedly()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        float duration = 4f;
        float flipInterval = 0.9f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            sr.flipX = !sr.flipX;
            yield return new WaitForSeconds(flipInterval);
            elapsed += flipInterval;
        }

        sr.flipX = false;
    }
}
