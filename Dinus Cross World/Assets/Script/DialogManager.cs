using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText; // UI Text untuk dialog
    public string[] idleDialogs; // Array dialog idle
    public string[] storyDialogs; // Array dialog story

    public GameObject dialogPanel; // Panel dialog (referensi dari NPCDialogInteraction)
    public GameObject namaPanel; // Nama NPC yang muncul di dialog
    public GameObject dialogTextObject;
    public GameObject dialogInteraction;

    private int currentIdleDialogIndex = 0; // Indeks dialog idle
    private int currentStoryDialogIndex = 0; // Indeks dialog story
    private bool isDialogInProgress = false; // Flag dialog sedang berlangsung
    private Coroutine typingCoroutine; // Menyimpan coroutine mengetik

    public float typingSpeed = 0.05f; // Kecepatan mengetik (detik per karakter)
    private bool isInStoryMode = false; // Status mode story

    private bool isDialogActive = false;


    private void Update()
    {
        // Jika tombol Space ditekan
        if (Input.GetKeyDown(KeyCode.Space) && !isDialogInProgress)
        {
            if (isInStoryMode)
            {
                NextStoryDialog();
            }
            else
            {
                NextIdleDialog();
            }
        }
    }

    public void SetDialogActive(bool state)
    {
        isDialogActive = state;
    }

    // Menampilkan dialog idle
    public void ShowIdleDialog()
    {
        if (isDialogActive) return; // **Cegah dialog dibuka lagi jika sudah aktif**

        SetDialogActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        currentIdleDialogIndex = 0; // Pastikan mulai dari dialog pertama
        dialogText.text = "";
        dialogPanel.SetActive(true);
        namaPanel.SetActive(true);
        dialogTextObject.SetActive(true);
        dialogInteraction.SetActive(false);
        isDialogInProgress = true;
        typingCoroutine = StartCoroutine(TypeText(idleDialogs[currentIdleDialogIndex]));
    }

    // Menampilkan dialog story
    public void ShowStoryDialog()
    {
        if (isDialogActive) return; // **Cegah dialog dibuka lagi jika sudah aktif**

        SetDialogActive(true);

        isInStoryMode = true; // Aktifkan mode story

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        currentStoryDialogIndex = 0; // Pastikan mulai dari dialog pertama
        dialogText.text = "";
        dialogPanel.SetActive(true);
        namaPanel.SetActive(true);
        dialogTextObject.SetActive(true);
        dialogInteraction.SetActive(false );
        isDialogInProgress = true;
        typingCoroutine = StartCoroutine(TypeText(storyDialogs[currentStoryDialogIndex]));

    }

    // Pindah ke dialog idle berikutnya
    private void NextIdleDialog()
    {
        currentIdleDialogIndex++;
        if (currentIdleDialogIndex < idleDialogs.Length)
        {
            typingCoroutine = StartCoroutine(TypeText(idleDialogs[currentIdleDialogIndex]));
        }
        else
        {
            CloseDialog(); // Tutup dialog jika semua dialog selesai
        }
    }

    // Pindah ke dialog story berikutnya
    private void NextStoryDialog()
    {
        currentStoryDialogIndex++;
        if (currentStoryDialogIndex < storyDialogs.Length)
        {
            typingCoroutine = StartCoroutine(TypeText(storyDialogs[currentStoryDialogIndex]));
        }
        else
        {
            isInStoryMode = false; // Kembali ke mode idle setelah dialog selesai
            CloseDialog(); // Tutup dialog

        }
    }

    // Coroutine untuk mengetik teks dengan efek mengetik
    private IEnumerator TypeText(string dialog)
    {
        dialogText.text = ""; // Reset teks sebelum mengetik ulang
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isDialogInProgress = false; // Tandai dialog selesai
    }

    // Menutup dialog dan reset indeks
    private void CloseDialog()
    {
        SetDialogActive(false);
        dialogPanel.SetActive(false);
        namaPanel.SetActive(false);
        dialogTextObject.SetActive(false);
        dialogText.text = ""; // Kosongkan teks


        currentIdleDialogIndex = 0; // Reset indeks idle
        currentStoryDialogIndex = 0; // Reset indeks story
        dialogInteraction.SetActive(true);
    }
}
