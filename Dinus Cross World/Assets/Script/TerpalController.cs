using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerpalController : MonoBehaviour
{
    public GameObject ToKantin;
    private bool isPlayerInRange = false;
    private bool isOpened = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("TerpalOpened", 0) == 1)
        {
            OpenDoor();
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("HasRuler", 0) == 1)
            {
                Debug.Log("Tombol E ditekan dalam jangkauan!");
                OpenDoor();
                PlayerPrefs.SetInt("TerpalOpened", 1);
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;

        // Aktifkan objek setelah pintu terbuka
        if (ToKantin != null)
        {
            ToKantin.SetActive(true);
        }

        Destroy(gameObject); // Hapus penghalang pintu
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}


