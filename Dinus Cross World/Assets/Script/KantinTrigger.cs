using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KantinTrigger : MonoBehaviour

{
    public TaskManager taskManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            taskManager.CompleteTask();
            Destroy(gameObject);
        }
    }
}
