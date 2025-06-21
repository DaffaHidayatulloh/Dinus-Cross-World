using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItemTahuOnDialog : MonoBehaviour
{
    public DialogManager dialogManager;

    private bool itemDeleted = false;

    void Update()
    {
        // Cek apakah dialog sedang aktif dan item belum dihapus
        if (!itemDeleted && dialogManager != null && dialogManager.IsDialogPanelActive())
        {
            if (PlayerPrefs.HasKey("HasTahu"))
            {
                PlayerPrefs.DeleteKey("HasTahu");
                Debug.Log("HasTahu telah dihapus karena dialog aktif.");
                itemDeleted = true; // Supaya hanya dihapus sekali
            }
        }
    }
}
