using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;
  private SpriteRenderer spriteRenderer;
  private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found!");
        }

        animator = GetComponent<Animator>();

        (Vector2 lastPosition, bool isFacingRight) = SaveManager.Instance.LoadPlayerState();
        if (lastPosition != Vector2.zero)
        {
            transform.position = lastPosition;
            Facing(!isFacingRight); // Membalik arah saat kembali ke scene sebelumnya
        }
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0));

        float moveHorizontal = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            SetAnimParam(true);
        }

        if (moveHorizontal == 0) 
        {
            SetAnimParam(false);
        }
       

        if (moveHorizontal > 0)
        {
            Facing(true); // Menghadap ke kanan
        }
        else if (moveHorizontal < 0)
        {
            Facing(false); // Menghadap ke kiri
        }
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
    private void SetAnimParam(bool IsMove)
    {
        animator.SetBool("IsMove", IsMove);
    }
    private void OnDestroy()
    {
        // Simpan posisi dan arah player sebelum berpindah scene
        SaveManager.Instance.SavePlayerState(transform.position, !spriteRenderer.flipX);
    }

}
