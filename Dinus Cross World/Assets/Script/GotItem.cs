using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotItem : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;               // Panel yang ingin ditutup
    public Button closeButton1;            // Tombol tutup 1
    public Button closeButton2;            // Tombol tutup 2
    public Text blinkingText;              // Legacy UI Text
    public GameObject Item;

    [Header("Blinking Text Settings")]
    public float blinkSpeed = 0.5f;        // Kecepatan kedap-kedip teks

    [HideInInspector] public bool isClosed = false;

    private void Start()
    {
        // Tambahkan listener ke kedua tombol
        if (closeButton1 != null)
            closeButton1.onClick.AddListener(ClosePanel);

        if (closeButton2 != null)
            closeButton2.onClick.AddListener(ClosePanel);

        // Mulai efek blink jika text tersedia
        if (blinkingText != null)
            StartCoroutine(BlinkText());
    }

    void ClosePanel()
    {
        if (panel != null)
            panel.SetActive(false);

        isClosed = true;

        Item.SetActive(true);
    }

    System.Collections.IEnumerator BlinkText()
    {
        while (true)
        {
            blinkingText.enabled = !blinkingText.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
