using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TahuPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private GameObject player;
    public TaskManager taskManager;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasTahu", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tombol E ditekan dalam jangkauan!");
            PlayerPrefs.SetInt("HasTahu", 1);

            if (player != null)
            {
                PlayerTextController textController = player.GetComponent<PlayerTextController>();
                if (textController != null)
                {
                    textController.ShowText("Aku menemukan tahu kuning!");
                    taskManager.CompleteTask();
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
        }
    }
}

