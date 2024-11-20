using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour

{
    public BoxCollider2D interactCollider;
    public KeyCode interactKey = KeyCode.E; // Key to trigger interaction
    public KeyCode toggleDialogKey = KeyCode.Space; // Key to temporarily hide and show dialog
    public DialogManager dialogManager; // Reference to the DialogManager script
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject Nama;

    private bool playerInRange = false;
    private bool isDialogVisible = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama.SetActive(false);
    }

    void Update()
    {
        // Check if player is in range and presses the interaction key
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            dialogManager.ShowIdleDialog();
            Nama.SetActive(true);
            isDialogVisible = true;
        }

        // Check if the player presses the toggle dialog key
        if (isDialogVisible && Input.GetKeyDown(toggleDialogKey))
        {
            StartCoroutine(ToggleDialogVisibility());
        }
    }

    // Check if player enters the interaction area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogPanel.SetActive(false);
            dialogText.SetActive(false);
            Nama?.SetActive(false);
            isDialogVisible = false;
        }
    }

    // Coroutine to toggle dialog visibility
    private IEnumerator ToggleDialogVisibility()
    {
        // Hide dialog
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama?.SetActive(false);

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Show dialog again
        dialogPanel.SetActive(true);
        dialogText.SetActive(true);
        Nama?.SetActive(true);
    }

    // Draw gizmo for interaction area in Scene View
    void OnDrawGizmosSelected()
    {
        if (interactCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(interactCollider.bounds.center, interactCollider.bounds.size);
        }
    }
}


