using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggerOnDialogComplete : MonoBehaviour
{
    public DialogManager dialogManager; // Referensi ke DialogManager
    public TaskManager taskManager; // Referensi ke TaskManager
    private bool taskCompleted = false;       // Agar hanya dijalankan sekali
    private bool isWatchingDialog = false;    // Mengetahui apakah kita sedang menunggu dialog selesai

    void Update()
    {
        if (isWatchingDialog && !taskCompleted)
        {
            // Cek apakah dialog sudah selesai & panel sudah ditutup
            if (!dialogManager.IsDialogInProgress() &&
                !dialogManager.IsTyping() &&
                !dialogManager.IsDialogPanelActive())
            {
                taskManager.CompleteTask();
                taskCompleted = true;
                isWatchingDialog = false;
            }
        }
    }

    // Fungsi ini dipanggil dari luar (misal saat mulai dialog), untuk mulai memantau status dialog
    public void BeginWatch()
    {
        isWatchingDialog = true;
    }
}
