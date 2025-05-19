using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnnouncementController : MonoBehaviour
{
    [Header("Header 1 & Text 1")]
    public Text header1Text;
    public CanvasGroup header1Group;
    public Text text1;
    public CanvasGroup text1Group;

    [Header("Header 2 & Text 2")]
    public GameObject header2Object;
    public Text header2Text;
    public CanvasGroup header2Group;

    public GameObject text2Object;
    public Text text2;
    public CanvasGroup text2Group;

    [Header("Settings")]
    public float fadeDuration = 1f;
    public float displayDuration = 2f;
    public string nextSceneName = "MainMenu";

    private void Start()
    {
        // Matikan Header 2 dan Text 2 di awal
        header2Object.SetActive(false);
        text2Object.SetActive(false);

        // Mulai sequence
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // Fade in Header 1 + Text 1
        StartCoroutine(FadeCanvasGroup(header1Group, 0, 1));
        yield return StartCoroutine(FadeCanvasGroup(text1Group, 0, 1));
        yield return new WaitForSeconds(displayDuration);
        StartCoroutine(FadeCanvasGroup(header1Group, 1, 0));
        yield return StartCoroutine(FadeCanvasGroup(text1Group, 1, 0));

        // Aktifkan Header 2 dan Text 2 sebelum ditampilkan
        header2Object.SetActive(true);
        text2Object.SetActive(true);

        // Fade in Header 2 + Text 2
        StartCoroutine(FadeCanvasGroup(header2Group, 0, 1));
        yield return StartCoroutine(FadeCanvasGroup(text2Group, 0, 1));
        yield return new WaitForSeconds(displayDuration);
        StartCoroutine(FadeCanvasGroup(header2Group, 1, 0));
        yield return StartCoroutine(FadeCanvasGroup(text2Group, 1, 0));

        // Pindah ke scene selanjutnya
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float start, float end)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            group.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        group.alpha = end;
    }
}
