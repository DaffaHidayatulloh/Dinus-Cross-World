using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName; // Nama scene tujuan
    private bool isPlayerInRange = false; // Apakah player berada dalam trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cek apakah yang masuk adalah player
        {
            isPlayerInRange = true; // Aktifkan flag saat player masuk area trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cek apakah yang keluar adalah player
        {
            isPlayerInRange = false; // Matikan flag saat player keluar area trigger
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Simpan posisi dan arah player sebelum berpindah
            SaveManager.Instance.SavePlayerState(GameObject.FindGameObjectWithTag("Player").transform.position,
                                                 !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX);
            
            AudioManager.instance.StopWalkSound();
            AudioManager.instance.StopSFX();

            SceneManager.LoadScene(sceneName);
        }
    }
}


