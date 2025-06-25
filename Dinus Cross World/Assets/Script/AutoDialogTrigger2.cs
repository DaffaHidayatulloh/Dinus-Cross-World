using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogTrigger2 : MonoBehaviour
{
    private NPCDialogInteraction dialogInteraction;
    private TaskManager taskManager;
    private bool dialogTriggered = false;

    [Header("Player Settings")]
    public GameObject playerObj;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;

    private void Start()
    {
        dialogInteraction = GetComponent<NPCDialogInteraction>();
        taskManager = FindObjectOfType<TaskManager>();

        // Backup assign jika belum di-assign lewat Inspector
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            if (playerMovement == null)
                playerMovement = playerObj.GetComponent<PlayerMovement>();

            if (playerAnimator == null)
                playerAnimator = playerObj.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && dialogInteraction != null && taskManager != null)
        {
            int currentTask = taskManager.GetCurrentTaskIndex();

            if (currentTask == 7 && dialogInteraction.useSpecialDialog)
            {
                // Hentikan movement dan ubah animasi ke idle
                if (playerMovement != null)
                    playerMovement.enabled = false;

                AudioManager.instance.StopWalkSound();

                if (playerAnimator != null)
                    playerAnimator.SetBool("IsMove", false); // Pastikan sesuai Animator

                if (!dialogTriggered)
                {
                    dialogTriggered = true;
                    StartCoroutine(TriggerAfterDelay());
                }
            }
        }
    }

    private System.Collections.IEnumerator TriggerAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        dialogInteraction.TriggerDialog();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogTriggered = false;

            // Aktifkan kembali movement saat keluar dari collider
            if (playerMovement != null)
                playerMovement.enabled = true;
        }
    }
}
