using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public Transform target; // Target yang akan diikuti (misalnya karakter yang dapat dimainkan)
    public float moveSpeed = 3f; // Kecepatan gerak NPC
    public float stoppingDistance = 2f; // Jarak di mana NPC berhenti

    private Rigidbody2D rb;
    private bool isFollowing = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
