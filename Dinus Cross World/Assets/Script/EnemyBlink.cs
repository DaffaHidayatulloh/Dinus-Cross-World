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
        float newX = transform.position.x + (direction * blinkDistance);

        if (newX < leftLimit.position.x)
        {
            newX = leftLimit.position.x;
            isChasing = false;
        }
        else if (newX > rightLimit.position.x)
        {
            newX = rightLimit.position.x;
            isChasing = false;
        }

        // Cek apakah musuh melewati pemain setelah blink, dan pastikan musuh berhenti di samping pemain
        if (CheckIfPlayerPassed(newX))
        {
            StopAtPlayer();
        }
        else
        {
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    private bool CheckIfPlayerPassed(float newX)
    {
        // Jika musuh berada di posisi yang melewati posisi pemain
        return (direction == 1 && newX >= player.position.x) || (direction == -1 && newX <= player.position.x);
    }

    private void StopAtPlayer()
    {
        // Tentukan posisi berhenti di samping pemain
        float stopX = (direction == 1) ? player.position.x - 1f : player.position.x + 1f; // 1f adalah jarak samping pemain

        // Pastikan musuh tidak melewati batas
        if (stopX < leftLimit.position.x)
        {
            stopX = leftLimit.position.x;
        }
        else if (stopX > rightLimit.position.x)
        {
            stopX = rightLimit.position.x;
        }

        transform.position = new Vector2(stopX, transform.position.y);
        isChasing = false; // Berhenti mengejar setelah sampai di samping pemain
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHide.IsHiding) return; // Tidak melakukan apa-apa jika pemain bersembunyi
            direction = (player.position.x > transform.position.x) ? 1 : -1;
            isChasing = true;
            waitTimer = waitTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Tetap mengejar walaupun pemain keluar dari trigger
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (PlayerHide.IsHiding) return; // Tidak melakukan apa-apa jika pemain bersembunyi
            //letakan game over screen disini
            Time.timeScale = 0f; // Freeze game hanya jika pemain tidak sedang bersembunyi
            Debug.Log("Player menyentuh Enemy! Time Freeze!");
        }
    }
}
