using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour

{
    public BoxCollider2D interactCollider;
    public KeyCode interactKey = KeyCode.E; // Key to trigger interaction
    public DialogManager dialogManager; // Reference to the DialogManager script
    public GameObject dialogPanel;
    public GameObject dialogText;

    private bool playerInRange = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
    }
    void Update()
    {
        // Check if player is in range and presses the interaction key
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            dialogManager.ShowIdleDialog();
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
        }
    }

    // Menggambar gizmo untuk menunjukkan area interaksi di Scene View
    void OnDrawGizmosSelected()
    {
        if (interactCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(interactCollider.bounds.center, interactCollider.bounds.size);
        }
    }
}

