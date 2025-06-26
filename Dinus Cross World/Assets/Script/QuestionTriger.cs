using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTriger : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.instance.PlaySFX(13);
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
