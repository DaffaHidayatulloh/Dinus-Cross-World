using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerpalController : MonoBehaviour
{
    public Text warningText;
    public GameObject ToKantin;
    public Image coverImage; // UI Image yang akan ditampilkan selama 3 detik
    private bool isPlayerInRange = false;
    private bool isOpened = false;
    private bool isWarningShowing = false;
    private CanvasGroup warningCanvasGroup;

    private void Start()
    {
        if (PlayerPrefs.GetInt("TerpalOpened", 0) == 1)
        {
            OpenDoor();
        }

        if (warningText != null)
        {
            warningCanvasGroup = warningText.GetComponent<CanvasGroup>();
        }

        if (coverImage != null)
        {
            coverImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("HasRuler", 0) == 1)
            {
                Debug.Log("Tombol E ditekan dalam jangkauan!");
                StartCoroutine(OpenDoorWithCover()); // Gunakan coroutine
                PlayerPrefs.SetInt("TerpalOpened", 1);
            }
            else
            {
                ShowWarning("Kamu butuh penggaris untuk membuka jalan ke Kantin!");
            }
        }
    }

    private IEnumerator OpenDoorWithCover()
    {
        // Tampilkan cover selama 3 detik
        if (coverImage != null)
        {
            coverImage.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        if (coverImage != null)
        {
            coverImage.gameObject.SetActive(false);
        }

        OpenDoor(); // Buka pintu setelah 3 detik
    }

    private void OpenDoor()
    {
        isOpened = true;

        if (ToKantin != null)
        {
            ToKantin.SetActive(true);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void ShowWarning(string message)
    {
        if (warningText != null && !isWarningShowing)
        {
            warningText.text = message;
            StopAllCoroutines(); // agar tidak overlap jika ditekan berulang
            StartCoroutine(AnimateWarning());
        }
    }

    private IEnumerator AnimateWarning()
    {
        isWarningShowing = true;

        warningText.gameObject.SetActive(true);
        warningCanvasGroup.alpha = 1f;

        RectTransform rectTransform = warningText.GetComponent<RectTransform>();
        Vector3 startPos = rectTransform.anchoredPosition;
        Vector3 endPos = startPos + new Vector3(0, 30f, 0);

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            warningCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        warningText.gameObject.SetActive(false);
        rectTransform.anchoredPosition = startPos;
        isWarningShowing = false;
    }
}



