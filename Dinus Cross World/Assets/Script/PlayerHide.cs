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
    private PlayerMovement playerMovement;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>(); // Ambil komponen kontrol
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
            if (playerMovement != null) playerMovement.enabled = false; // Nonaktifkan kontrol
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Normal
            Shadow.gameObject.SetActive(true);
            Player2.gameObject.SetActive(false);
            if (playerMovement != null) playerMovement.enabled = true; // Aktifkan kontrol
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HideSpot"))
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


