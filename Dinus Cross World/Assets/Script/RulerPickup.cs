using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;

    void Start()
    {
        // Jika kunci sudah diambil sebelumnya, maka hilangkan objeknya
        if (PlayerPrefs.GetInt("HasRuler", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tombol E ditekan dalam jangkauan!");
            PlayerPrefs.SetInt("HasRuler", 1);
            Destroy(gameObject);
        }
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

