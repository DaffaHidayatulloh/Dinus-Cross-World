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
    private string prefsKey;

    private void Start()
    {
        prefsKey = gameObject.name + "_Triggered";

        if (PlayerPrefs.GetInt(prefsKey, 0) == 1)
        {
            gameObject.SetActive(false); // Sudah terpanggil sebelumnya
        }

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Sembunyikan teks di awal
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player") && messageText != null)
        {
            AudioManager.instance.PlaySFX(8);
            hasTriggered = true;
            PlayerPrefs.SetInt(prefsKey, 1); // Simpan status sudah dipicu
            PlayerPrefs.Save();
            StopAllCoroutines();
            StartCoroutine(ShowMessagesRoutine());
        }
    }

    private IEnumerator ShowMessagesRoutine()
    {
        messageText.gameObject.SetActive(true);

        // Tampilkan pesan pertama dengan efek shake
        yield return StartCoroutine(ShowSingleMessage(message1, true));
        yield return StartCoroutine(ShowSingleMessage(message2, false));

        messageText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private IEnumerator ShowSingleMessage(string msg, bool doShake)
    {
        messageText.text = msg;

        // Set alpha ke 0 sebelum fade in
        Color color = messageText.color;
        color.a = 0;
        messageText.color = color;

        Coroutine fadeCoroutine = StartCoroutine(FadeText(0, 1));

        if (doShake)
        {
            StartCoroutine(ShakeText(1.5f, 5f)); // Jalankan shake bersamaan
        }

        yield return fadeCoroutine;

        yield return new WaitForSeconds(showDuration);
        yield return StartCoroutine(FadeText(1, 0));
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

    private IEnumerator ShakeText(float duration, float strength)
    {
        Vector3 originalPos = messageText.rectTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;
            messageText.rectTransform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        messageText.rectTransform.localPosition = originalPos;
    }
}

