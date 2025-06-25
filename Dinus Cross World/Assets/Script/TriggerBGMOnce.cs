using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBGMOnce : MonoBehaviour
{
    public TaskManager taskManager; // Drag dari scene ke Inspector
    public string playerTag = "Player";
    public int requiredTaskIndex = 5; // Task: "Berbicara Dengan Pak Hamadi di Kelas D1.4"
    public string bgmPlayKey = "BGM_PakHamadiSudahDiputar";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (PlayerPrefs.HasKey(bgmPlayKey)) return;

        if (taskManager != null && taskManager.GetCurrentTaskIndex() == requiredTaskIndex)
        {
            AudioManager.instance?.PlayBGM(1);
            PlayerPrefs.SetInt(bgmPlayKey, 1);
            PlayerPrefs.Save();
        }
    }
}

