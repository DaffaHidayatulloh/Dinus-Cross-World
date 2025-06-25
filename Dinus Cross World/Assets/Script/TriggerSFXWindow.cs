using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFXWindow : MonoBehaviour
{
    public TaskManager taskManager;
    public string playerTag = "Player";
    public int requiredTaskIndex = 2; // Task "Bawa Tahu Kuning ke Pak Satpam"

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;
        if (!other.CompareTag(playerTag)) return;

        if (taskManager != null && taskManager.GetCurrentTaskIndex() == requiredTaskIndex)
        {
            AudioManager.instance?.PlaySFX(2);
            hasPlayed = true;
        }
    }
}



