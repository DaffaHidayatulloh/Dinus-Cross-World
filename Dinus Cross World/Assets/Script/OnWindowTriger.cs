using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWindowTriger : MonoBehaviour
{
    public TaskManager taskManager; // Referensi ke TaskManager
    public int targetTaskIndex = 2; // Index untuk task "Bawa Tahu Kuning ke Pak Satpam"
    public GameObject objectJendela; // GameObject kosong yang mengandung collider
    public GameObject objectJendela2;
    public GameObject objectJendela3;

    private void Start()
    {
        SetObjectsActive(false);
    }

    private void Update()
    {
        if (taskManager == null)
            return;

        bool shouldBeActive = taskManager.GetCurrentTaskIndex() == targetTaskIndex;
        SetObjectsActive(shouldBeActive);
    }
    private void SetObjectsActive(bool isActive)
    {
        if (objectJendela != null)
            objectJendela.SetActive(isActive);

        if (objectJendela2 != null)
            objectJendela2.SetActive(isActive);

        if (objectJendela3 != null)
            objectJendela3.SetActive(isActive);
    }
}
