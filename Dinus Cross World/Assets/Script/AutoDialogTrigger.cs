using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogTrigger : MonoBehaviour

{
    public GameObject npcObject;
    public CinemachineVirtualCamera virtualCamera;

    [Header("Zoom Settings")]
    public float zoomSize = 2f;
    public float zoomDuration = 0.1f; // Lebih cepat (jumpscare-style)
    public float zoomHoldTime = 0.3f;

    [Header("Player Settings")]
    public PlayerMovement playerMovement;
    public Animator playerAnimator;

    private bool hasActivated = false;
    private float originalSize;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && npcObject != null && !hasActivated)
        {
            hasActivated = true;

            if (!npcObject.activeSelf)
            {
                npcObject.SetActive(true);
                AudioManager.instance.PlaySFX(3);
                StartCoroutine(ZoomAndReturn(other));
            }
            else
            {
                StartCoroutine(TriggerDialogNextFrame(other));
            }
        }
    }

    private IEnumerator ZoomAndReturn(Collider2D playerCollider)
    {
        if (virtualCamera == null)
        {
            Debug.LogWarning("VirtualCamera not assigned!");
            yield break;
        }

        // Simpan ukuran kamera asli
        originalSize = virtualCamera.m_Lens.OrthographicSize;

        // Disable gerakan pemain & set animasi idle
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerAnimator != null)
            playerAnimator.SetBool("IsMove", false);

        // Zoom in (cepat)
        float t = 0;
        while (t < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(originalSize, zoomSize, t / zoomDuration);
            t += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = zoomSize;

        yield return new WaitForSeconds(zoomHoldTime); // Tahan sebentar

        // Zoom out (cepat)
        t = 0;
        while (t < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomSize, originalSize, t / zoomDuration);
            t += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = originalSize;

        // Aktifkan lagi gerakan pemain
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Lanjutkan ke dialog
        StartCoroutine(TriggerDialogNextFrame(playerCollider));
    }

    private IEnumerator TriggerDialogNextFrame(Collider2D playerCollider)
    {
        yield return null;

        NPCDialogInteraction npcDialog = npcObject.GetComponent<NPCDialogInteraction>();
        if (npcDialog != null)
        {
            npcDialog.SendMessage("OnTriggerEnter2D", playerCollider, SendMessageOptions.DontRequireReceiver);
            npcDialog.TriggerDialog();
        }
    }
}
