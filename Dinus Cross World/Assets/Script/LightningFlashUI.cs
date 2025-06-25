using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningFlashUI : MonoBehaviour
{
    public Image targetImage;           // Drag UI Image ke sini
    public float flashAlpha = 0.5f;     // Alpha saat "kilat"
    public float flashDuration = 0.1f;  // Durasi flash
    public string lightningKey = "LightningTriggered"; // Key unik untuk PlayerPrefs

    private bool isFlashing = false;

    void Start()
    {
        // Jika sudah pernah trigger sebelumnya, nonaktifkan script
        if (PlayerPrefs.GetInt(lightningKey, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFlashing && PlayerPrefs.GetInt(lightningKey, 0) == 0)
        {
            StartCoroutine(FlashEffect());
            AudioManager.instance.PlaySFX(1);

            // Simpan status sebagai sudah pernah trigger
            PlayerPrefs.SetInt(lightningKey, 1);
            PlayerPrefs.Save();
        }
    }

    IEnumerator FlashEffect()
    {
        isFlashing = true;

        Color originalColor = targetImage.color;
        Color flashColor = originalColor;
        flashColor.a = flashAlpha;
        targetImage.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        flashColor.a = 0f;
        targetImage.color = flashColor;

        isFlashing = false;
    }
}


