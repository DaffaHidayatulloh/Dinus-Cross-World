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

    public GameObject dialogIndicator; // Objek indikator (tanpa teks)

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
        // Cek hanya jika pemain dalam range dan dialog belum aktif
        if (playerInRange && Input.GetKeyDown(interactKey) && !isDialogActive)
        {
            isDialogActive = true;
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            Nama.SetActive(true);

            dialogManager.ShowIdleDialog();
            dialogIndicator.SetActive(false); // Sembunyikan indikator setelah interaksi
        }
    }

    // Cek jika pemain masuk area interaksi
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Tampilkan indikator hanya jika dialog belum aktif
            if (!isDialogActive)
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
            dialogIndicator.SetActive(false); // Sembunyikan indikator saat pemain keluar area interaksi
            isDialogActive = false; // Reset status dialog saat keluar area
        }
    }
}


