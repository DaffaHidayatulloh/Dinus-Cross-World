using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogTrigger : MonoBehaviour
{
    public GameObject npcObject;               // NPC yang akan diaktifkan

    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && npcObject != null && !hasActivated)
        {
            hasActivated = true;

            // Aktifkan NPC dulu jika belum aktif
            if (!npcObject.activeSelf)
            {
                npcObject.SetActive(true);
            }

            // Tunggu satu frame sebelum memicu dialog untuk memastikan NPC sempat aktif
            StartCoroutine(TriggerDialogNextFrame());
        }
    }

    private System.Collections.IEnumerator TriggerDialogNextFrame()
    {
        yield return null; // tunggu 1 frame setelah npc aktif

        NPCDialogInteraction npcDialog = npcObject.GetComponent<NPCDialogInteraction>();
        if (npcDialog != null)
        {
            // Paksa set playerInRange ke true secara manual
            npcDialog.SendMessage("OnTriggerEnter2D", GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), SendMessageOptions.DontRequireReceiver);

            // Baru trigger dialog
            npcDialog.TriggerDialog();
        }
    }
}


