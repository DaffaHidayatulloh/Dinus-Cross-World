using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBgmOnce2 : MonoBehaviour
{
    public TaskManager taskManager;
    public string playerTag = "Player";
    public int targetTaskIndex = 4;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag(playerTag)) return;
        if (taskManager == null || AudioManager.instance == null) return;

        if (taskManager.GetCurrentTaskIndex() == targetTaskIndex)
        {
            if (AudioManager.instance.GetCurrentBGMIndex() != 1)
            {
                AudioManager.instance.PlayBGM(1);
                hasTriggered = true;
            }
        }
    }
}


