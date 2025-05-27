using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextController : MonoBehaviour
{
    public Text uiText; // assign di Inspector ke UI Text
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = uiText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiText.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        uiText.gameObject.SetActive(false);
    }

    public void ShowText(string message)
    {
        uiText.text = message;
        uiText.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        // Fade In
        yield return StartCoroutine(Fade(0f, 1f, 0.3f)); // durasi 0.3 detik
        yield return new WaitForSeconds(2f); // tampil selama 2 detik
                                             // Fade Out
        yield return StartCoroutine(Fade(1f, 0f, 0.3f));
        uiText.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        canvasGroup.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}


