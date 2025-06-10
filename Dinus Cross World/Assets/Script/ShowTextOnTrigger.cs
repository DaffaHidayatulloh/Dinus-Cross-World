using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnTrigger : MonoBehaviour
{
    public Text messageText; // Legacy UI Text
    [TextArea]
    public string message1 = "Pesan pertama!";
    [TextArea]
    public string message2 = "Pesan kedua!";
    public float fadeDuration = 1f;
    public float showDuration = 1f;

    private bool hasTriggered = false;

    private void Start()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Sembunyikan teks di awal
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player") && messageText != null)
        {
            hasTriggered = true;
            StopAllCoroutines();
            StartCoroutine(ShowMessagesRoutine());
        }
    }

    private IEnumerator ShowMessagesRoutine()
    {
        messageText.gameObject.SetActive(true); // Tampilkan teks
        yield return StartCoroutine(ShowSingleMessage(message1));
        yield return StartCoroutine(ShowSingleMessage(message2));
        messageText.gameObject.SetActive(false); // Sembunyikan teks lagi
        gameObject.SetActive(false); // Nonaktifkan pemicu agar tidak bisa disentuh lagi
    }

    private IEnumerator ShowSingleMessage(string msg)
    {
        messageText.text = msg;

        // Set alpha ke 0 sebelum fade in
        Color color = messageText.color;
        color.a = 0;
        messageText.color = color;

        yield return StartCoroutine(FadeText(0, 1)); // Fade in
        yield return new WaitForSeconds(showDuration);
        yield return StartCoroutine(FadeText(1, 0)); // Fade out
        messageText.text = "";
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = messageText.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            messageText.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        messageText.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
