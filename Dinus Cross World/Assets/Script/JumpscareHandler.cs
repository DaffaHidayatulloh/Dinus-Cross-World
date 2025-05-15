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

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonPressed);
        }
    }

    private void OnContinueButtonPressed()
    {
        if (player != null)
        {
            player.position = respawnPosition; // Reset posisi player
        }

        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(false); // Sembunyikan jumpscare
        }

        // Reset game state lainnya jika perlu, misal musuh berhenti mengejar
        // atau nyalakan kembali kontrol player
    }
}


