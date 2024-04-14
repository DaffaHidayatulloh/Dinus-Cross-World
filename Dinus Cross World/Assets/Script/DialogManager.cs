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

    // Index to keep track of current dialog in idleDialogs array
    private int currentIdleDialogIndex = 0;

    private bool isDialogInProgress = false; // Flag to prevent dialog from being skipped too quickly



    private void Update()
    {
        // Check for input to advance to the next idle dialog
        if (Input.GetKeyDown(KeyCode.Space) && !isDialogInProgress)
        {
            NextIdleDialog();
        }
    }

    // Method to show the next idle dialog
    private void NextIdleDialog()
    {
        currentIdleDialogIndex++;

        if (currentIdleDialogIndex < idleDialogs.Length)
        {
            ShowIdleDialog();
        }
        else
        {
            // Reset the index to loop back to the start of the array
            currentIdleDialogIndex = 0;
            ShowIdleDialog();
        }
    }

    // Method to display the current idle dialog
    public void ShowIdleDialog()
    {
        isDialogInProgress = true; // Mark dialog as in progress to prevent skipping

        // Set the text of the UI Text component to the current idle dialog
        dialogText.text = idleDialogs[currentIdleDialogIndex];

        // Start a coroutine to wait for a short time before allowing the dialog to be skipped
        StartCoroutine(DialogInProgress());
    }

    // Coroutine to mark dialog as no longer in progress after a short delay
    private System.Collections.IEnumerator DialogInProgress()
    {
        yield return new WaitForSeconds(0.5f); // Adjust as needed
        isDialogInProgress = false;
    }
}
