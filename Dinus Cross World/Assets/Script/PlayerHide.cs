using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHide : MonoBehaviour
{
    public static bool IsHiding = false;
    public KeyCode hideKey = KeyCode.E; // Tombol untuk bersembunyi
    private bool isHiding = false;
    private bool canHide = false;
    private SpriteRenderer spriteRenderer;
    public GameObject Shadow;
    public GameObject Player2;
    private Collider2D playerCollider;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canHide && Input.GetKeyDown(hideKey))
        {
            ToggleHide();
        }
    }

    private void ToggleHide()
    {
        isHiding = !isHiding;
        IsHiding = isHiding;
        if (isHiding)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.0f); // Transparan
            Shadow.gameObject.SetActive(false);
            Player2.gameObject.SetActive(true);
            //animator.SetBool("isMoving", false); // Berhenti bergerak
            //animator.SetBool("isHiding", true); // Aktifkan animasi sembunyi jika ada
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Kembali normal
            Shadow.gameObject.SetActive(true);
            Player2.gameObject.SetActive(false);
            //animator.SetBool("isHiding", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HideSpot")) // Pastikan tempat sembunyi memiliki tag "HideSpot"
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HideSpot"))
        {
            canHide = false;
            if (isHiding)
            {
                ToggleHide();
            }
        }
    }
}


