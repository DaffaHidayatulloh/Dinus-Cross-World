using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorRainZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!IndoorRainZoneTracker.instance.isInRainZone)
        {
            AudioManager.instance.PlayRainSound(1);
        }

        IndoorRainZoneTracker.instance.isInRainZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        IndoorRainZoneTracker.instance.isInRainZone = false;
        AudioManager.instance.PlayRainSound(0);
    }
}

