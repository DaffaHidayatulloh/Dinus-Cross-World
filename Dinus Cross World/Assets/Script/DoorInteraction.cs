using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public Text warningText;

    [Header("Game Objects")]
    public GameObject nextArea; // Objek tujuan yang muncul setelah pintu terbuka

    private bool isPlayerInRange = false;
    private bool isOpened = false;
    private CanvasGroup warningCanvasGroup;

    void Start()
    {
        if (PlayerPrefs.GetInt("DoorOpened", 0) == 1)
        {
            OpenDoorInstantly();
        }

        if (warningText != null)
        {
            warningCanvasGroup = warningText.GetComponent<CanvasGroup>();
            warningText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("HasKey", 0) == 1)
            {
                PlayerPrefs.SetInt("DoorOpened", 1);
                OpenDoor();
                PlayerPrefs.DeleteKey("HasKey");
            }
            else
            {
                ShowWarning("Kamu butuh kunci untuk membuka pintu ini!");
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;

        if (nextArea != null)
        {
            nextArea.SetActive(true);
        }

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }

    private void OpenDoorInstantly()
    {
        isOpened = true;

        if (nextArea != null)
        {
            nextArea.SetActive(true);
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
        if (warningText != null && !warningText.gameObject.activeSelf)
        {
            warningText.text = message;
            StartCoroutine(AnimateWarning());
        }
    }

    private IEnumerator AnimateWarning()
    {
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
    }
}


