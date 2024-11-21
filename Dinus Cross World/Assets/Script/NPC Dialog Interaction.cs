using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour
{
    public BoxCollider2D interactCollider;
    public KeyCode interactKey = KeyCode.E; // Key to trigger interaction
    public KeyCode toggleDialogKey = KeyCode.Space;
    public DialogManager dialogManager; // Reference to the DialogManager script
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject Nama;

    private bool playerInRange = false;
    public bool isInStory = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama.SetActive(false);
    }
    void Update()
    {
        // Jika pemain berada dalam area dan menekan tombol interaksi
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            Nama.SetActive(true);

            if (isInStory)
            {
                dialogManager.ShowStoryDialog();
            }
            else
            {
                dialogManager.ShowIdleDialog();
            }
        }

    }

    // Check if player enters the interaction area
    void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang masuk collider adalah pemain
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Memeriksa apakah objek yang keluar collider adalah pemain
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogPanel.SetActive(false);
            dialogText.SetActive(false);
            Nama?.SetActive(false);
        }
    }
}

