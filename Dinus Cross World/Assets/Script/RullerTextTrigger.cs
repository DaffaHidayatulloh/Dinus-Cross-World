using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RullerTextTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player") && PlayerPrefs.GetInt("HasRuler", 0) == 1)
        {
            GameObject player = other.gameObject;

            if (player != null)
            {
                PlayerTextController textController = player.GetComponent<PlayerTextController>();
                if (textController != null)
                {
                    textController.ShowText("Siapa wanita tadi, dia cantik sekali.");
                    hasTriggered = true;
                }
                else
                {
                    Debug.LogWarning("PlayerTextController tidak ditemukan pada player.");
                }
            }
        }
    }
}
