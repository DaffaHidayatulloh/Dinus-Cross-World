using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroller : MonoBehaviour
{
    public RectTransform creditText;     // RectTransform dari teks credit
    public float scrollSpeed = 30f;      // Kecepatan gerak ke atas
    public float resetPositionY = -500f; // Posisi awal (di luar layar bawah)
    public float endPositionY = 500f;    // Posisi akhir (di luar layar atas)

    void Start()
    {
        // Atur posisi awal
        creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, resetPositionY);
    }

    void Update()
    {
        // Geser ke atas
        creditText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Reset ke bawah jika sudah lewat batas atas
        if (creditText.anchoredPosition.y >= endPositionY)
        {
            creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, resetPositionY);
        }
    }
}
