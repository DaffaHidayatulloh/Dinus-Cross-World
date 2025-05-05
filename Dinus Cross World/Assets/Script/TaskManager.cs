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

    public NPCDialogInteraction npcSatpam;
    public NPCDialogInteraction npcClara;
    // Tambahkan NPC lain jika ada

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

        UpdateNPCInteractable();
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
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = taskRect.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(-200, 0, 0);

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

        UpdateNPCInteractable();  // Panggil update NPC

        // Fade in
        elapsed = 0f;
        Vector3 enterStart = endPos + new Vector3(400, 0, 0);
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

    private void UpdateNPCInteractable()
    {
        //Default semua NPC tidak bisa diajak bicara
        npcSatpam.SetInteractable(false);
        npcClara.SetInteractable(false);
        // Tambahkan reset untuk NPC lain jika ada

        if (currentTaskIndex == 0)
        {
            npcSatpam.SetInteractable(true); //Hanya Satpam yang aktif
        }
        else if (currentTaskIndex == 1)
        {
            npcClara.SetInteractable(true); //Hanya Clara yang aktif
        }
        // Tambahkan untuk task lainnya jika ada
    }
}

