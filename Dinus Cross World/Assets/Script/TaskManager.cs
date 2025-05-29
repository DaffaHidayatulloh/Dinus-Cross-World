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
        "Bawa Tahu Kuning ke Pak Satpam",
        "Cari Informasi Keberadaan Kepingan Artifact",
        "Cari Kunci yang Tergeletak di Depan Aula",
        "Berbicara Dengan Pak Hamadi di Kelas D1.4",
        "Kerjakan Soal dari Pak Hamadi"
    };

    private NPCDialogInteraction npcSatpam;
    private NPCDialogInteraction npcClara;

    void Start()
    {
        taskRect = taskText.GetComponent<RectTransform>();
        canvasGroup = taskText.GetComponent<CanvasGroup>();

        currentTaskIndex = PlayerPrefs.GetInt("TaskIndex", 0);
        taskText.text = tasks[currentTaskIndex];

        FindNPCsInScene();
        UpdateNPCInteractable();
    }

    private void FindNPCsInScene()
    {
        GameObject satpamObj = GameObject.FindGameObjectWithTag("Satpam");
        if (satpamObj != null)
            npcSatpam = satpamObj.GetComponent<NPCDialogInteraction>();

        GameObject claraObj = GameObject.FindGameObjectWithTag("Clara");
        if (claraObj != null)
            npcClara = claraObj.GetComponent<NPCDialogInteraction>();
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
            taskText.text = "";
        }

        UpdateNPCInteractable();

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
        if (npcSatpam != null)
        {
            npcSatpam.enabled = (currentTaskIndex == 0 || currentTaskIndex == 2);

            if (currentTaskIndex == 2)
                npcSatpam.useSpecialDialog = true;
            else
                npcSatpam.useSpecialDialog = false;
        }

        if (npcClara != null)
        {
            npcClara.enabled = (currentTaskIndex == 1 || currentTaskIndex == 4);

            if (currentTaskIndex == 4)
                npcClara.useSpecialDialog = true;
            else
                npcClara.useSpecialDialog = false;
        }
    }

    public void SetTaskIndex(int index)
    {
        if (index >= 0 && index < tasks.Length)
        {
            currentTaskIndex = index;
            PlayerPrefs.SetInt("TaskIndex", currentTaskIndex);
            PlayerPrefs.Save();

            taskText.text = tasks[currentTaskIndex];
            UpdateNPCInteractable();
        }
    }
    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }
}


