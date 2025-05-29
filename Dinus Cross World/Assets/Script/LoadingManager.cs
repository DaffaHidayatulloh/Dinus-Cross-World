using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar; // Optional
    public Text loadingText; // Optional
    public string sceneToLoad; // Nama scene tujuan

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float progress = 0f;

        // Tahap 1: Naik ke 50%
        while (progress < 0.5f)
        {
            progress += Time.deltaTime * 0.2f; // Kecepatan loading ke 50%
            UpdateUI(progress);
            yield return null;
        }

        progress = 0.5f;
        UpdateUI(progress);

        // Tahan di 50% selama 2 detik
        yield return new WaitForSeconds(2f);

        // Tahap 2: Lanjut ke 100%
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.25f; // Kecepatan ke 100%
            UpdateUI(progress);
            yield return null;
        }

        progress = 1f;
        UpdateUI(progress);

        yield return new WaitForSeconds(0.5f); // Tambahan delay opsional
        operation.allowSceneActivation = true;
    }

    void UpdateUI(float progress)
    {
        if (progressBar != null)
            progressBar.value = progress;
        if (loadingText != null)
            loadingText.text = "Loading... " + (int)(progress * 100f) + "%";
    }

}
