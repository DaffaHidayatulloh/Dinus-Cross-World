using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnd2 : MonoBehaviour
{
    public GameObject player;
    public GameObject player2; // Player pengganti
    public Animator cutsceneAnimator;
    public float fallSpeed = 10f;
    public float appearDuration = 0.2f;
    public GameObject objectToActivateAfterCutscene;
    public float fadeInDuration = 1f;

    private Vector3 startFallPosition;
    private Vector3 targetPosition;
    private SpriteRenderer playerRenderer;
    private bool isFalling = false;
    private float alpha = 0f;

    void Start()
    {
        playerRenderer = player.GetComponent<SpriteRenderer>();
        targetPosition = player.transform.position;

        if (cutsceneAnimator != null)
            cutsceneAnimator.gameObject.SetActive(false);

        if (objectToActivateAfterCutscene != null)
            objectToActivateAfterCutscene.SetActive(false);

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
        while (isFalling)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, fallSpeed * Time.deltaTime);
            if (Vector3.Distance(player.transform.position, targetPosition) < 0.01f)
            {
                player.transform.position = targetPosition;
                isFalling = false;
            }
            yield return null;
        }

        if (cutsceneAnimator != null)
        {
            cutsceneAnimator.gameObject.SetActive(true);
           // cutsceneAnimator.SetTrigger("PlayCutscene2");

            yield return new WaitForSeconds(2f);

            if (objectToActivateAfterCutscene != null)
            {
                objectToActivateAfterCutscene.SetActive(true);
                yield return StartCoroutine(FadeInObject(objectToActivateAfterCutscene));

                // Setelah fade in selesai, ganti player
                if (player != null) player.SetActive(false);
                if (player2 != null) player2.SetActive(true);
            }
        }
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
}



