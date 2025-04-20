using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour

{
    public Text taskText;
    private int currentTaskIndex = 0;

    private string[] tasks = {
        "Pergi ke kantin!",
        "Jelajahi kampus!"
    };

    void Start()
    {
        // Load task index dari PlayerPrefs
        currentTaskIndex = PlayerPrefs.GetInt("TaskIndex", 0);
        UpdateTaskText();
    }

    public void CompleteTask()
    {
        currentTaskIndex++;
        // Simpan task index ke PlayerPrefs
        PlayerPrefs.SetInt("TaskIndex", currentTaskIndex);
        PlayerPrefs.Save();

        if (currentTaskIndex < tasks.Length)
        {
            UpdateTaskText();
        }
        else
        {
            taskText.text = "Semua tugas selesai!";
        }
    }

    void UpdateTaskText()
    {
        if (currentTaskIndex < tasks.Length)
        {
            taskText.text = "" + tasks[currentTaskIndex];
        }
        else
        {
            taskText.text = "Semua tugas selesai!";
        }
    }
}
