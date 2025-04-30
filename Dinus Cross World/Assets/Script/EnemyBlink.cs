using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class EnemyBlink : MonoBehaviour
{
    public Transform player;
    public float blinkDistance = 2f;
    public float waitTime = 1.5f;
    public Transform leftLimit;
    public Transform rightLimit;
    public Image jumpscareImage;

    private bool isChasing = false;
    private float waitTimer = 0f;
    private int direction = 0;

    private Collider2D enemyCollider;
    private bool hasPassedHiddenPlayer = false;

    private void Start()
    {
        enemyCollider = GetComponent<Collider2D>();
    }

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

        // Nonaktifkan collider jika player sedang bersembunyi
        if (PlayerHide.IsHiding)
        {
            if (!hasPassedHiddenPlayer)
            {
                // Nonaktifkan collider jika belum lewat
                if (enemyCollider.enabled)
                    enemyCollider.enabled = false;

                // Cek apakah musuh baru saja melewati player
                if ((direction == 1 && newX > player.position.x + 0.5f) ||
                    (direction == -1 && newX < player.position.x - 0.5f))
                {
                    hasPassedHiddenPlayer = true;
                    if (!enemyCollider.enabled)
                        enemyCollider.enabled = true;
                }
            }
        }
        else
        {
            // Reset status jika player tidak sembunyi
            hasPassedHiddenPlayer = false;
            if (!enemyCollider.enabled)
                enemyCollider.enabled = true;
        }

        // Cek apakah musuh harus berhenti di samping player
        if (CheckIfPlayerPassed(newX))
        {
            if (!PlayerHide.IsHiding)
            {
                StopAtPlayer();
                return;
            }
        }

        transform.position = new Vector2(newX, transform.position.y);
    }

    private bool CheckIfPlayerPassed(float newX)
    {
        return (direction == 1 && newX >= player.position.x) ||
               (direction == -1 && newX <= player.position.x);
    }

    private void StopAtPlayer()
    {
        float stopX = (direction == 1) ? player.position.x - 1f : player.position.x + 1f;

        if (stopX < leftLimit.position.x)
            stopX = leftLimit.position.x;
        else if (stopX > rightLimit.position.x)
            stopX = rightLimit.position.x;

        transform.position = new Vector2(stopX, transform.position.y);
        isChasing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHide.IsHiding) return;
            direction = (player.position.x > transform.position.x) ? 1 : -1;
            isChasing = true;
            waitTimer = waitTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Tetap mengejar
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (PlayerHide.IsHiding) return;
            Time.timeScale = 0f;
            //masukan jumpscare screen disini
            jumpscareImage.gameObject.SetActive(true);
            Debug.Log("Player menyentuh Enemy! Time Freeze!");
        }
    }
}
