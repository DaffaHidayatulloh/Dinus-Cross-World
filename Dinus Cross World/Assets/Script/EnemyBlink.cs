using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlink : MonoBehaviour
{

    public Transform player;           // Referensi ke pemain
    public float blinkDistance = 2f;   // Jarak teleportasi per blink
    public float waitTime = 1.5f;      // Waktu tunggu sebelum blink berikutnya
    public Transform leftLimit;        // Batas kiri pergerakan musuh
    public Transform rightLimit;       // Batas kanan pergerakan musuh

    private bool isChasing = false;    // Status apakah musuh sedang mengejar
    private float waitTimer = 0f;      // Timer untuk menghitung waktu tunggu
    private int direction = 0;         // Arah pergerakan: -1 untuk kiri, 1 untuk kanan

    private void Update()
    {
        if (isChasing)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                Blink();
                waitTimer = waitTime;
            }
        }
    }

    private void Blink()
    {
        // Hitung posisi baru berdasarkan arah dan jarak blink
        float newX = transform.position.x + (direction * blinkDistance);

        // Pastikan posisi baru tidak melewati batas kiri atau kanan
        if (newX < leftLimit.position.x)
        {
            newX = leftLimit.position.x;
            isChasing = false; // Berhenti mengejar jika mencapai batas kiri
        }
        else if (newX > rightLimit.position.x)
        {
            newX = rightLimit.position.x;
            isChasing = false; // Berhenti mengejar jika mencapai batas kanan
        }

        // Perbarui posisi musuh
        transform.position = new Vector2(newX, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHide.IsHiding) return;
            // Tentukan arah berdasarkan posisi relatif pemain
            direction = (player.position.x > transform.position.x) ? 1 : -1;
            isChasing = true;
            waitTimer = waitTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Tidak melakukan apa-apa saat pemain keluar dari collider
        // Musuh tetap mengejar hingga mencapai batas
    }
}
