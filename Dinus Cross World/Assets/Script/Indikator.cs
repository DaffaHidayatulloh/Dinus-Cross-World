using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indikator : MonoBehaviour
{
    public GameObject indicator; // Drag & Drop indikator dari Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(true); // Munculkan indikator saat player masuk radius
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false); // Sembunyikan indikator saat keluar radius
        }
    }
}