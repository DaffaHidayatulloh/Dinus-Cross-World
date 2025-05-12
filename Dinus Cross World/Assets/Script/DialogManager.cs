using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText; // UI Text untuk dialog
    public string[] idleDialogs; // Array dialog idle

    public GameObject dialogPanel; // Panel dialog (referensi dari NPCDialogInteraction)
    public GameObject namaPanel; // Nama NPC yang muncul di dialog
    public GameObject dialogTextObject;

    private int currentIdleDialogIndex = 0; // Indeks dialog idle
    private bool isDialogInProgress = false; // Flag dialog sedang berlangsung
    private Coroutine typingCoroutine; // Menyimpan coroutine mengetik

    public float typingSpeed = 0.05f; // Kecepatan mengetik (detik per karakter)
    private bool isDialogActive = false;
    private bool isTyping = false;

    public static event System.Action OnDialogFinished;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;


    private void Update()
    {
        // Jika tombol Space ditekan
        if (Input.GetKeyDown(KeyCode.Space) && !isDialogInProgress && !isTyping)
        {
            NextIdleDialog();
        }
    }

    public void SetDialogActive(bool state)
    {
        isDialogActive = state;

        if (playerMovement != null)
        {
            playerMovement.enabled = !state; // Nonaktifkan/aktifkan player movement
        }

        if (playerAnimator != null && state)
        {
            playerAnimator.SetBool("IsMove", false); // Pastikan parameternya sesuai Animator kamu
        }
    }

    // Menampilkan dialog idle
    public void ShowIdleDialog()
    {
        if (isDialogActive) return; // Cegah dialog dibuka lagi jika sudah aktif

        SetDialogActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        currentIdleDialogIndex = 0; // Mulai dari dialog pertama
        dialogText.text = "";
        dialogPanel.SetActive(true);
        namaPanel.SetActive(true);
        dialogTextObject.SetActive(true);
        isDialogInProgress = true;
        typingCoroutine = StartCoroutine(TypeText(idleDialogs[currentIdleDialogIndex]));
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
            currentIdleDialogIndex = 0;
        }
    }

    // Coroutine untuk mengetik teks dengan efek mengetik
    private IEnumerator TypeText(string dialog)
    {
        isTyping = true;
        dialogText.text = ""; // Reset teks sebelum mengetik ulang
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isDialogInProgress = false; // Tandai dialog selesai
        isTyping = false;
    }

    // Menutup dialog dan reset indeks
    private void CloseDialog()
    {
        SetDialogActive(false);
        dialogPanel.SetActive(false);
        namaPanel.SetActive(false);
        dialogTextObject.SetActive(false);
        dialogText.text = ""; // Kosongkan teks

        currentIdleDialogIndex = 0; // Reset indeks
        OnDialogFinished?.Invoke();
    }

    public void ShowCustomDialog(string[] customDialogs)
    {
        if (isDialogActive) return;

        SetDialogActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        currentIdleDialogIndex = 0;
        dialogText.text = "";
        dialogPanel.SetActive(true);
        namaPanel.SetActive(true);
        dialogTextObject.SetActive(true);
        isDialogInProgress = true;
        typingCoroutine = StartCoroutine(TypeCustomText(customDialogs));
    }

    private IEnumerator TypeCustomText(string[] dialogs)
    {
        foreach (string dialog in dialogs)
        {
            dialogText.text = "";
            foreach (char letter in dialog.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && !isTyping);
        }

        CloseDialog();
        currentIdleDialogIndex = 0;
    }
    public bool IsDialogInProgress()
    {
        return isDialogInProgress;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
    public bool IsDialogPanelActive()
    {
        return dialogPanel != null && dialogPanel.activeSelf;
    }
}
