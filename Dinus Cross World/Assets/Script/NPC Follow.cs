using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour

{
    public Transform target; // Target yang akan diikuti (misalnya karakter yang dapat dimainkan)
    public float moveSpeed = 3f; // Kecepatan gerak NPC
    public float stoppingDistance = 2f; // Jarak di mana NPC berhenti
    public BoxCollider2D npcCollider; // Collider NPC

    private Rigidbody2D rb;
    private bool isFollowing = true;
    private bool isPassable = false; // Apakah NPC dapat dilewati saat karakter di atasnya

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Membuat collider NPC tidak aktif saat awal permainan
        npcCollider.enabled = false;
    }

    private void Update()
    {
        if (target != null && isFollowing)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > stoppingDistance)
            {
                Vector2 direction = target.position - transform.position;
                rb.velocity = direction.normalized * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Method untuk mengatur apakah NPC akan mengikuti player atau tidak berdasarkan radius
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }

    // Method untuk mengatur apakah NPC dapat dilewati atau tidak
    public void SetPassable(bool passable)
    {
        isPassable = passable;
        // Mengaktifkan atau menonaktifkan collider NPC berdasarkan nilai isPassable
        npcCollider.enabled = !passable;
    }

    // Ketika karakter berada di atas NPC, NPC akan dilewati
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetPassable(true);
        }
    }

    // Ketika karakter meninggalkan area di atas NPC, NPC tidak akan dilewati lagi
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetPassable(false);
        }
    }
}
