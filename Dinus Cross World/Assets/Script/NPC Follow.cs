using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour

{
    public Transform target; // Target yang akan diikuti (misalnya karakter yang dapat dimainkan)
    public float moveSpeed = 3f; // Kecepatan gerak NPC
    public float stoppingDistance = 2f; // Jarak di mana NPC berhenti

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isFollowing = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // Membuat collider NPC tidak aktif saat awal permainan
        
    }

    private void Update()
    {
        //Fungsi Following
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

        //Fungsi Facing
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        if (moveHorizontal > 0)
        {
            Facing(true); // Menghadap ke kanan
        }
        else if (moveHorizontal < 0)
        {
            Facing(false); // Menghadap ke kiri
        }
    }

    // Method untuk mengatur apakah NPC akan mengikuti player atau tidak berdasarkan radius
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }

    
    private void Facing(bool isFacingRight)
    {
        // Fungsi untuk membalik player
        if (isFacingRight)
        {
            spriteRenderer.flipX = false; // Tidak flip
        }
        else
        {
            spriteRenderer.flipX = true; // Melakukan flip pada sumbu X
        }
    }
}
