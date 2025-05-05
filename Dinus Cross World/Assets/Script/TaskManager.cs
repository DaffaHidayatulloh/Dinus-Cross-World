using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour

{
    public Text taskText;
    private RectTransform taskRect;
    private CanvasGroup canvasGroup;

    private int currentTaskIndex = 0;

    private string[] tasks = {
        "Pergi Berbicara Dengan Pak Satpam",
        "Dapatkan Tahu Kuning di Kantin",
        "Cari Informasi Keberadaan Kepingan Artifact",
        "Cari Kunci yang Tergeletak di Depan Aula",
        "Berbicara Dengan Pak Khamadi di Kelas D1.4"
    };

    void Start()
    {
        taskRect = taskText.GetComponent<RectTransform>();
        canvasGroup = taskText.GetComponent<CanvasGroup>();

        currentTaskIndex = PlayerPrefs.GetInt("TaskIndex", 0);
        taskText.text = tasks[currentTaskIndex];
    }

    public void CompleteTask()
    {
        if (currentTaskIndex < tasks.Length)
        {
            StartCoroutine(AnimateTaskChange());
        }
    }

    IEnumerator AnimateTaskChange()
    {
        // ANIMASI KELUAR: Geser ke kiri & fade out
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = taskRect.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(-200, 0, 0); // Geser ke kiri

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            taskRect.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            canvasGroup.alpha = 1 - t;
            elapsed += Time.deltaTime;
            yield return null;
        }

        taskRect.anchoredPosition = endPos;
        canvasGroup.alpha = 0;

        // Ganti teks jika masih ada task
        currentTaskIndex++;
        PlayerPrefs.SetInt("TaskIndex", currentTaskIndex);
        PlayerPrefs.Save();

        if (currentTaskIndex < tasks.Length)
        {
            taskText.text = tasks[currentTaskIndex];
        }
        else
        {
            taskText.text = "Semua tugas selesai!";
        }

        // Reset posisi ke kanan & fade in
        elapsed = 0f;
        Vector3 enterStart = endPos + new Vector3(400, 0, 0); // Muncul dari kanan
        Vector3 enterEnd = startPos;
        taskRect.anchoredPosition = enterStart;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            taskRect.anchoredPosition = Vector3.Lerp(enterStart, enterEnd, t);
            canvasGroup.alpha = t;
            elapsed += Time.deltaTime;
            yield return null;
        }

        taskRect.anchoredPosition = enterEnd;
        canvasGroup.alpha = 1;
    }
}
