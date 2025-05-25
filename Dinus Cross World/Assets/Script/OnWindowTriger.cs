using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWindowTriger : MonoBehaviour
{
    public TaskManager taskManager; // Referensi ke TaskManager
    public GameObject objectJendela; // GameObject kosong yang mengandung collider
    public GameObject objectJendela2;
    public GameObject objectJendela3;
    private int[] validTaskIndices = { 2, 3 };


    private void Start()
    {
        SetObjectsActive(false);
    }

    private void Update()
    {
        if (taskManager == null)
            return;

        int currentTask = taskManager.GetCurrentTaskIndex();
        bool shouldBeActive = IsValidTaskIndex(currentTask);
        SetObjectsActive(shouldBeActive);
    }
    private bool IsValidTaskIndex(int index)
    {
        foreach (int validIndex in validTaskIndices)
        {
            if (index == validIndex)
                return true;
        }
        return false;
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
