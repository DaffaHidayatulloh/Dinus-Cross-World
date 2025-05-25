using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneIntroGondolruwo : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineVirtualCamera cutsceneCamera;
    public float cutsceneDuration = 3f;

    [Header("Player")]
    public GameObject playerMovementObject; // <--- Selalu dibuat public
    public Animator playerAnimator;

    public EnemyBlink[] enemiesToDisable;

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
    }
}
