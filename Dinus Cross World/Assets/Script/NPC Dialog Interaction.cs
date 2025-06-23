using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public DialogManager dialogManager;
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject Nama;

    public GameObject dialogIndicator;
    public string[] specialDialogs;
    public bool useSpecialDialog = false;

    public SpriteRenderer npcSpriteRenderer;

    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;

    private bool isDialogActive = false;
    private bool playerInRange = false;
    private bool fromCollider2 = false;
    private bool fromCollider1 = false;

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
            TriggerDialog();
            AudioManager.instance.StopWalkSound();
        }
    }

    public void TriggerDialog()
    {
        if (!isDialogActive && playerInRange)
        {
            isDialogActive = true;
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            Nama.SetActive(true);


            if (npcSpriteRenderer != null)
            {
                if (fromCollider2)
                {
                    npcSpriteRenderer.flipX = true;
                }
                else if (fromCollider1)
                {
                    npcSpriteRenderer.flipX = false;
                }
            }

            TaskTriggerOnDialogComplete taskTrigger = GetComponent<TaskTriggerOnDialogComplete>();
            if (taskTrigger != null) taskTrigger.BeginWatch();

            if (useSpecialDialog && specialDialogs.Length > 0)
            {
                dialogManager.ShowCustomDialog(specialDialogs);
            }
            else
            {
                dialogManager.ShowIdleDialog();
            }

            dialogIndicator.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            fromCollider1 = boxCollider1 != null && other.IsTouching(boxCollider1);
            fromCollider2 = boxCollider2 != null && other.IsTouching(boxCollider2);

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

            fromCollider1 = false;
            fromCollider2 = false;
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





