using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneEnd2 : MonoBehaviour
{
    public GameObject player;
    public GameObject player2; // Player pengganti
    public Animator cutsceneAnimator;
    public float fallSpeed = 10f;
    public float appearDuration = 0.2f;
    public GameObject[] panelSequence; // Panel-panel yang akan difade-in satu per satu
    public GameObject objectToActivateAfterCutscene;
    public float fadeInDuration = 1f;
    public GameObject panelBesar; // Panel besar yang difade-in terakhir
    public GameObject panelTandaTanya;
    public GameObject imageFadeOut;
    public float fadeOutDuration = 1f;

    private Vector3 startFallPosition;
    private Vector3 targetPosition;
    private SpriteRenderer playerRenderer;
    private bool isFalling = false;
    private float alpha = 0f;

    [Header("Scale Effect Settings")]
    public float scaleMultiplier = 1.2f; // Ukuran akhir relatif terhadap ukuran asli
    public float scaleDuration = 0.3f;   // Lama efek scale


    void Start()
    {
        playerRenderer = player.GetComponent<SpriteRenderer>();
        targetPosition = player.transform.position;

        if (cutsceneAnimator != null)
            cutsceneAnimator.gameObject.SetActive(false);

        if (objectToActivateAfterCutscene != null)
            objectToActivateAfterCutscene.SetActive(false);

        if (panelSequence != null)
        {
            foreach (var panel in panelSequence)
                if (panel != null) panel.SetActive(false);
        }

        if (player2 != null)
            player2.SetActive(false);

        startFallPosition = targetPosition + new Vector3(0, 5f, 0);
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        playerRenderer.color = new Color(1f, 1f, 1f, 0f);
        player.SetActive(false);

        yield return new WaitForSeconds(1f);

        player.transform.position = startFallPosition;
        player.SetActive(true);
        playerRenderer.color = new Color(1f, 1f, 1f, 0f);

        float timer = 0f;
        while (timer < appearDuration)
        {
            timer += Time.deltaTime;
            alpha = Mathf.Clamp01(timer / appearDuration);
            playerRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        isFalling = true;
        bool animatorActivated = false;
        while (isFalling)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, fallSpeed * Time.deltaTime);
            if (!animatorActivated && cutsceneAnimator != null)
            {
                cutsceneAnimator.gameObject.SetActive(true);
                animatorActivated = true;
            }
            if (Vector3.Distance(player.transform.position, targetPosition) < 0.01f)
            {
                player.transform.position = targetPosition;
                isFalling = false;
            }
            yield return null;
        }

        if (cutsceneAnimator != null)
        {

            // Tunggu 0.2 detik, baru aktifkan player2
            yield return new WaitForSeconds(0.2f);
            if (player2 != null) player2.SetActive(true);
            player.SetActive(false);

            // Lanjutkan tunggu 1 detik lagi (total 2 detik sejak animator aktif)
            yield return new WaitForSeconds(0.5f);

            // Aktifkan panel satu per satu
            // Aktifkan panel-panel satu per satu dengan efek fade-in
            if (panelSequence != null)
            {
                foreach (var panel in panelSequence)
                {
                    if (panel != null)
                    {
                        panel.SetActive(true);
                        yield return StartCoroutine(FadeInObject(panel));
                    }
                }

                // Tunggu 1 detik sebelum fade out semua panel
                yield return new WaitForSeconds(1f);

                // Mulai fade out semua panel secara bersamaan
                List<Coroutine> fadeOutObject = new List<Coroutine>();
                foreach (var panel in panelSequence)
                {
                    if (panel != null)
                    {
                        fadeOutObject.Add(StartCoroutine(FadeOutObject(panel)));
                    }
                }
                foreach (var co in fadeOutObject)
                {
                    yield return co;
                }

                // Setelah semua panel difade out, nonaktifkan mereka
                foreach (var panel in panelSequence)
                {
                    if (panel != null) panel.SetActive(false);
                }
            }

            // Aktifkan panel besar
            if (panelBesar != null)
            {
                panelBesar.SetActive(true);
                StartCoroutine(ShakeObject(panelBesar));
            }
            yield return new WaitForSeconds(1f);
            if (panelTandaTanya != null)
            {
                panelTandaTanya.SetActive(true);
                StartCoroutine(ShakeObject(panelTandaTanya));
            }
            yield return new WaitForSeconds(1.5f);
            List<Coroutine> fadeOuts = new List<Coroutine>();

            if (panelBesar != null)
                fadeOuts.Add(StartCoroutine(FadeOutObject(panelBesar)));

            if (panelTandaTanya != null)
                fadeOuts.Add(StartCoroutine(FadeOutObject(panelTandaTanya)));

            foreach (var co in fadeOuts)
                yield return co;

            // Setelah fade out selesai, nonaktifkan kedua panel
            if (panelBesar != null)
                panelBesar.SetActive(false);

            if (panelTandaTanya != null)
                panelTandaTanya.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            if (objectToActivateAfterCutscene != null)
            {
                objectToActivateAfterCutscene.SetActive(true);
                yield return StartCoroutine(FadeInWithScale(objectToActivateAfterCutscene));
            }

            yield return new WaitForSeconds(1f);

            if (imageFadeOut != null)
            {
                imageFadeOut.SetActive(true);
                yield return StartCoroutine(FadeInObject(imageFadeOut));
            }
            SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator FadeOutObject(GameObject target)
    {
        CanvasGroup cg = target.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            float t = 0f;
            float startAlpha = cg.alpha;
            while (t < fadeOutDuration)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Lerp(startAlpha, 0f, t / fadeOutDuration);
                yield return null;
            }
            cg.alpha = 0f;
            yield break;
        }

        SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float t = 0f;
            Color originalColor = sr.color;
            while (t < fadeOutDuration)
            {
                t += Time.deltaTime;
                float a = Mathf.Lerp(originalColor.a, 0f, t / fadeOutDuration);
                sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
                yield return null;
            }
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            yield break;
        }

        Debug.LogWarning("FadeOutObject: Target tidak memiliki CanvasGroup atau SpriteRenderer.");
    }
    IEnumerator FadeInObject(GameObject target)
    {
        CanvasGroup cg = target.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = 0f;
            float t = 0f;
            while (t < fadeInDuration)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Clamp01(t / fadeInDuration);
                yield return null;
            }
            yield break;
        }

        SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float t = 0f;
            Color originalColor = sr.color;
            while (t < fadeInDuration)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / fadeInDuration);
                sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
                yield return null;
            }
            yield break;
        }

        Debug.LogWarning("FadeInObject: Target tidak memiliki CanvasGroup atau SpriteRenderer.");
    }
    IEnumerator ShakeObject(GameObject target, float duration = 0.3f, float magnitude = 10f)
    {
        RectTransform rt = target.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogWarning("ShakeObject: Target tidak memiliki RectTransform.");
            yield break;
        }

        Vector3 originalPos = rt.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            rt.anchoredPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.anchoredPosition = originalPos;
    }
    IEnumerator FadeInWithScale(GameObject target)
    {
        // Ambil atau tambahkan CanvasGroup jika belum ada
        CanvasGroup cg = target.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = target.AddComponent<CanvasGroup>();
        }

        cg.alpha = 0f;

        Transform tf = target.transform;
        Vector3 originalScale = tf.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;

        float t = 0f;
        while (t < scaleDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / scaleDuration);

            // Fade-in & Scale bersamaan
            cg.alpha = normalized;
            tf.localScale = Vector3.Lerp(originalScale, targetScale, normalized);

            yield return null;
        }

        // Pastikan hasil akhir akurat
        cg.alpha = 1f;
        tf.localScale = targetScale;
    }


}




