using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggerOnDialogComplete : MonoBehaviour

    {
        public DialogManager dialogManager; // Referensi ke DialogManager
        public TaskManager taskManager; // Referensi ke TaskManager
        private bool taskCompleted = false; // Agar hanya dijalankan sekali
        private bool isWatchingDialog = false; // Mengetahui apakah kita sedang menunggu dialog selesai

        private void OnEnable()
        {
            DialogManager.OnDialogFinished += HandleDialogFinished;
        }

        private void OnDisable()
        {
            DialogManager.OnDialogFinished -= HandleDialogFinished;
        }

        public void BeginWatch()
        {
            isWatchingDialog = true;
            taskCompleted = false;
        }

        private void HandleDialogFinished()
        {
            if (isWatchingDialog && !taskCompleted)
            {
                taskManager.CompleteTask();
                taskCompleted = true;
                isWatchingDialog = false;
            }
        }
    }
