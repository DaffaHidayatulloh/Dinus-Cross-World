using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHideStory : MonoBehaviour
{
    public TaskManager taskManager; // Referensi ke TaskManager
    public int targetTaskIndex = 2; // Index untuk task "Bawa Tahu Kuning ke Pak Satpam"
    public GameObject objectToActivate; // GameObject kosong yang mengandung collider

    private void Start()
    {
        if (objectToActivate != null)
            objectToActivate.SetActive(false); // Mulai dalam keadaan nonaktif
    }

    private void Update()
    {
        if (taskManager == null || objectToActivate == null)
            return;

        // Aktifkan hanya saat task yang sesuai sedang aktif
        bool shouldBeActive = taskManager.GetCurrentTaskIndex() == targetTaskIndex;
        objectToActivate.SetActive(shouldBeActive);
    }
}
