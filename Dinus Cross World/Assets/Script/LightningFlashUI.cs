using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningFlashUI : MonoBehaviour

{
    public Image targetImage;           // Drag UI Image ke sini
    public float flashAlpha = 0.5f;     // Alpha saat "kilat"
    public float flashDuration = 0.1f;  // Durasi flash (semakin kecil, semakin cepat)

    private bool isFlashing = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFlashing)
        {
            StartCoroutine(FlashEffect());
            AudioManager.instance.PlaySFX(1);
        }
    }

    IEnumerator FlashEffect()
    {
        isFlashing = true;

        // Simpan warna awal
        Color originalColor = targetImage.color;

        // Atur alpha ke flashAlpha (misal 0.5f)
        Color flashColor = originalColor;
        flashColor.a = flashAlpha;
        targetImage.color = flashColor;

        // Tunggu sebentar
        yield return new WaitForSeconds(flashDuration);

        // Kembalikan alpha ke 0 (transparan)
        flashColor.a = 0f;
        targetImage.color = flashColor;

        isFlashing = false;
    }
}

