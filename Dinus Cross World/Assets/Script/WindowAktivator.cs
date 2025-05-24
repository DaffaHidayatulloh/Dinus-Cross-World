using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAktivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject objectToDeactivate2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // pastikan Player memiliki tag "Player"
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);

            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);

            if (objectToDeactivate2 != null)
                objectToDeactivate2.SetActive(false);
        }
    }
}