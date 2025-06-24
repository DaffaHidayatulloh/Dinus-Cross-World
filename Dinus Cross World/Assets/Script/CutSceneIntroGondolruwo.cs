using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneIntroGondolruwo : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineVirtualCamera cutsceneCamera;
    public float cutsceneDuration = 3f;

    [Header("Player")]
    public GameObject playerMovementObject; // <--- Selalu dibuat public
    public Animator playerAnimator;

    public EnemyBlink[] enemiesToDisable;

    [Header("Text")]
    public Text legacyUIText;
    public float fadeDuration = 1f;
    public Vector3 targetScale = Vector3.one;

    public void PlayKeyPickupCutscene()
    {
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        if (playerMovementObject != null)
        {
            // Nonaktifkan movement
            var movement = playerMovementObject.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = false;

            // Set animasi ke idle
            if (playerAnimator != null)
                playerAnimator.SetBool("IsMove", false);
        }

        foreach (EnemyBlink enemy in enemiesToDisable)
        {
            if (enemy != null)
                enemy.enabled = false;
        }

        if (cutsceneCamera != null)
            cutsceneCamera.Priority = 20;

        yield return new WaitForSeconds(cutsceneDuration);
        AudioManager.instance.PlaySFX(3);

        if (cutsceneCamera != null)
            cutsceneCamera.Priority = 0;

        // Aktifkan kembali movement
        if (playerMovementObject != null)
        {
            var movement = playerMovementObject.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = true;
        }
        foreach (EnemyBlink enemy in enemiesToDisable)
        {
            if (enemy != null)
                enemy.enabled = true;
        }
        StartCoroutine(FadeInUIText());
    }
    private IEnumerator FadeInUIText()
    {
        if (legacyUIText == null)
            yield break;

        legacyUIText.gameObject.SetActive(true);

        Color baseColor = legacyUIText.color;
        float halfDuration = fadeDuration * 0.5f;

        Vector3 startScale = Vector3.zero;
        Vector3 originalPosition = legacyUIText.rectTransform.localPosition;
        legacyUIText.rectTransform.localScale = startScale;

        float timer = 0f;
        float shakeStrength = 5f;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            legacyUIText.rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // Shake effect (ringan)
            float shakeX = Random.Range(-1f, 1f) * shakeStrength;
            float shakeY = Random.Range(-1f, 1f) * shakeStrength;
            legacyUIText.rectTransform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0);

            float alpha;
            if (t < 0.5f)
                alpha = Mathf.Lerp(0f, 1f, t * 2f); // Fade in
            else
                alpha = Mathf.Lerp(1f, 0f, (t - 0.5f) * 2f); // Fade out

            legacyUIText.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        legacyUIText.rectTransform.localPosition = originalPosition; // Reset posisi
        legacyUIText.gameObject.SetActive(false);
    }



}
