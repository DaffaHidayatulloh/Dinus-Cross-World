using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour
{
    public BoxCollider2D interactCollider;
    public KeyCode interactKey = KeyCode.E; // Tombol untuk interaksi
    public DialogManager dialogManager; // Referensi ke DialogManager
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject Nama;

    public GameObject dialogIndicator; // Indikator dialog yang muncul ketika pemain dekat NPC
    public Text indicatorText; // Teks yang ditampilkan di indikator

    private bool isDialogActive = false;
    private bool playerInRange = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.SetActive(false);
        Nama.SetActive(false);
        dialogIndicator.SetActive(false); // Indikator dimulai dalam keadaan tidak aktif
    }

    void Update()
    {
        isDialogActive = false;

        // Jika pemain berada dalam area dan menekan tombol interaksi
        if (playerInRange && Input.GetKeyDown(interactKey) && !isDialogActive)
        {
            isDialogActive = true;
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            Nama.SetActive(true);

            dialogManager.ShowIdleDialog();
            dialogIndicator.SetActive(false); // Sembunyikan indikator setelah interaksi
        }
        else if (playerInRange)
        {
            indicatorText.text = "Press E to Talk"; // Instruksi interaksi
        }
    }

    // Cek jika pemain masuk area interaksi
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            dialogIndicator.SetActive(true); // Tampilkan indikator saat pemain dekat NPC
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDialogActive = false;
            playerInRange = false;
            dialogPanel.SetActive(false);
            dialogText.SetActive(false);
            Nama?.SetActive(false);
            dialogIndicator.SetActive(false); // Sembunyikan indikator saat pemain keluar area interaksi
        }
    }
}
