using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Reference to the UI Text component
    public Text dialogText;

    // Array for storing dialog texts for idle dialog
    public string[] idleDialogs;
    public string[] StoryDialogs;

    // Index to keep track of current dialog in idleDialogs array
    private int currentIdleDialogIndex = 0;
    private int currentStoryDialogIndex = 0;

    private bool isDialogInProgress = false; // Flag to prevent dialog from being skipped too quickly

    private Coroutine typingCoroutine; // Menyimpan coroutine mengetik

    public float typingSpeed = 0.05f; // Kecepatan mengetik (dalam detik per karakter)
    private bool isInStoryMode = false;



    private void Update()
    {
        // Check for input to advance to the next idle dialog
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

    public void ShowStoryDialog()
    {
        isInStoryMode = true;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogText.text = "";
        isDialogInProgress = true;
        typingCoroutine = StartCoroutine(TypeText(StoryDialogs[currentStoryDialogIndex]));

    }

    // Method to show the next idle dialog
    private void NextIdleDialog()
    {
        currentIdleDialogIndex++;
        if (currentIdleDialogIndex >= idleDialogs.Length)
        {
            currentIdleDialogIndex = 0; // Reset jika mencapai akhir
        }

        ShowIdleDialog();
    }

    private void NextStoryDialog()
    {
        currentStoryDialogIndex++;
        if (currentStoryDialogIndex < StoryDialogs.Length)
        {
            ShowStoryDialog();
        }
        else
        {
            // Jika dialog story habis, keluar dari mode story
            isInStoryMode = false;
            currentStoryDialogIndex = 0; // Reset ke dialog pertama jika diperlukan
        }
    }

    // Method to display the current idle dialog
    public void ShowIdleDialog()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Hentikan coroutine sebelumnya jika ada
        }

        dialogText.text = ""; // Kosongkan teks sebelum mengetik ulang
        isDialogInProgress = true; // Tandai dialog sedang berlangsung
        typingCoroutine = StartCoroutine(TypeText(idleDialogs[currentIdleDialogIndex]));
    }

    // Coroutine untuk mengetik teks satu per satu
    private IEnumerator TypeText(string dialog)
    {
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter; // Tambahkan huruf ke teks
            yield return new WaitForSeconds(typingSpeed); // Tunggu sesuai kecepatan mengetik
        }

        isDialogInProgress = false; // Tandai dialog selesai
    }
}
