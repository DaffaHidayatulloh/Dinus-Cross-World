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
    private bool isInteractable = true; // Tambahkan ini

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama.SetActive(false);
        dialogIndicator.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !isDialogActive && isInteractable) //tambah isInteractable
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
            if (!isDialogActive && isInteractable) //cek juga isInteractable
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

    // Tambahkan ini untuk mengontrol dari TaskManager
    public void SetInteractable(bool state)
    {
        isInteractable = state;
        if (!isInteractable)
        {
            dialogIndicator.SetActive(false); // Matikan indikator jika tidak bisa diajak bicara
        }
    }
}



