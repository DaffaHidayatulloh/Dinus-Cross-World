using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorRainZoneTracker : MonoBehaviour
{
    public static IndoorRainZoneTracker instance;

    public bool isInRainZone = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
