using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private GameObject player;
    public TaskManager taskManager;

    public GameObject Gondolruwo;
    public CutSceneIntroGondolruwo cutsceneManager;

    void Start()
    {
        if (Gondolruwo != null)
            Gondolruwo.SetActive(false);

        if (PlayerPrefs.GetInt("HasKey", 0) == 1)
        {
            gameObject.SetActive(false); // Jangan destroy, cukup nonaktifkan
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tombol E ditekan dalam jangkauan!");
            PlayerPrefs.SetInt("HasKey", 1); // Simpan bahwa kunci sudah diambil

            if (player != null)
            {
                PlayerTextController textController = player.GetComponent<PlayerTextController>();
                if (textController != null)
                {
                    textController.ShowText("Aku menemukan sebuah kunci.");
                    taskManager.CompleteTask();
                }
            }
            if (Gondolruwo != null)
            {
                Gondolruwo.SetActive(true); // Aktifkan object lain
            }

            if (cutsceneManager != null)
                cutsceneManager.PlayKeyPickupCutscene();

            gameObject.SetActive(false); // Nonaktifkan key saat diambil
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

