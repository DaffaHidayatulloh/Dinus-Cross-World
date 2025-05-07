using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour
{
    public BoxCollider2D interactCollider;
    public KeyCode interactKey = KeyCode.E;
    public DialogManager dialogManager;
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject Nama;

    public GameObject dialogIndicator; // Objek indikator (tanpa teks)

    private bool isDialogActive = false;
    private bool playerInRange = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama.SetActive(false);
        dialogIndicator.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !isDialogActive)
        { 
            isDialogActive = true;
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            Nama.SetActive(true);

            dialogManager.ShowIdleDialog();
            dialogIndicator.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isDialogActive && enabled) 
            {
                dialogIndicator.SetActive(true);
            }
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
            dialogIndicator.SetActive(false);
            isDialogActive = false;
        }
    }

    private void OnEnable()
    {
        if (dialogIndicator != null)
            dialogIndicator.SetActive(false);
    }

    private void OnDisable()
    {
        if (dialogIndicator != null)
            dialogIndicator.SetActive(false);
    }

}



