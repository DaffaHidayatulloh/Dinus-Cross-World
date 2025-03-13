using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour

{
    private void Start()
    {
        // Cek apakah ada posisi tersimpan di scene ini, jika ada, load posisi terakhir
        Vector2 lastPosition = SaveManager.Instance.LoadPlayerPosition();
        if (lastPosition != Vector2.zero) // Hindari posisi default (0,0)
        {
            transform.position = new Vector3(lastPosition.x, lastPosition.y, transform.position.z);
        }
    }

    private void OnDestroy()
    {
        // Simpan posisi saat keluar dari scene
        SaveManager.Instance.SavePlayerPosition(transform.position);
    }
}

