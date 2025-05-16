using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private GameObject player;
    public TaskManager taskManager;

    void Start()
    {
        // Cek apakah tahu sudah diambil sebelumnya
        if (PlayerPrefs.GetInt("HasKey", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tombol E ditekan dalam jangkauan!");
            PlayerPrefs.SetInt("HasKey", 1); // Menandai bahwa key sudah diambil

            if (player != null)
            {
                PlayerTextController textController = player.GetComponent<PlayerTextController>();
                if (textController != null)
                {
                    textController.ShowText("Aku menemukan sebuah kunci.");
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

