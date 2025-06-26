using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColider : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayBGM(3);
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.StopBGM();
            objectToActivate.SetActive(false);
        }
    }
}