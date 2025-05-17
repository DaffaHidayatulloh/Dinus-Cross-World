using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpscareHandler : MonoBehaviour

{
    public Button continueButton;
    public Transform player;
    public Vector2 respawnPosition; // Titik posisi ulang player
    public GameObject jumpscarePanel; // Panel jumpscare yang ingin dimatikan

    public EnemyBlink enemyScript; // Referensi ke script EnemyBlink
    public Vector2 enemyRespawnPosition; // Posisi ulang enemy
    public GameObject keyObject;
    public TaskManager taskManager;


    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonPressed);
        }
    }

    private void OnContinueButtonPressed()
    {
        // Reset posisi player
        if (player != null)
        {
            player.position = respawnPosition;
        }

        // Reset posisi enemy dan state pengejaran
        if (enemyScript != null)
        {
            enemyScript.transform.position = enemyRespawnPosition;
            enemyScript.ResetChaseState();
        }

        PlayerPrefs.DeleteKey("HasKey");
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }

        if (taskManager != null)
        {
            taskManager.SetTaskIndex(4);
        }

        // Nonaktifkan panel jumpscare
        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(false);
        }
    }
}

