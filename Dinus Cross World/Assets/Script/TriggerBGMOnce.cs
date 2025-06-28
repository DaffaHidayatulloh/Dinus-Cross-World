using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBGMOnce : MonoBehaviour
{
    public TaskManager taskManager;
    public string playerTag = "Player";

    public int taskIndexForBGM1 = 5;
    public int taskIndexForBGM3 = 4;

    private bool hasPlayedBGM1 = false;
    private bool hasPlayedBGM3 = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (taskManager == null || AudioManager.instance == null) return;

        int currentTask = taskManager.GetCurrentTaskIndex();

        if (currentTask == taskIndexForBGM3 && !hasPlayedBGM3)
        {
            if (AudioManager.instance.GetCurrentBGMIndex() != 3)
            {
                AudioManager.instance.PlayBGM(3);
                hasPlayedBGM3 = true;
            }
        }
        else if (currentTask == taskIndexForBGM1 && !hasPlayedBGM1)
        {
            if (AudioManager.instance.GetCurrentBGMIndex() != 1)
            {
                AudioManager.instance.PlayBGM(1);
                hasPlayedBGM1 = true;
            }
        }
    }
}




