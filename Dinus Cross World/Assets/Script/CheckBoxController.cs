using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxController : MonoBehaviour
{
    [System.Serializable]
    public class AnswerButton
    {
        public Button button;
        public GameObject checkmarkOverlay; // UI Image di atas button
    }

    public AnswerButton[] answerButtons; // Isi semua button dan overlay-nya di Inspector
    public int correctIndex = 0;         // Index jawaban yang benar
    public Text resultText;              // UI Text untuk hasil

    void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // Capture index
            answerButtons[i].button.onClick.AddListener(() => OnButtonClicked(index));
            answerButtons[i].checkmarkOverlay.SetActive(false); // Pastikan semua overlay tidak aktif saat awal
        }
    }

    void OnButtonClicked(int index)
    {
        // Nonaktifkan semua overlay terlebih dahulu
        foreach (var ab in answerButtons)
        {
            ab.checkmarkOverlay.SetActive(false);
        }

        // Aktifkan overlay pada button yang dipilih
        answerButtons[index].checkmarkOverlay.SetActive(true);

        // Cek jawaban
        if (index == correctIndex)
        {
            Debug.Log("Jawaban Benar!");
            if (resultText != null)
                resultText.text = "Jawaban Benar!";
        }
        else
        {
            Debug.Log("Jawaban Salah!");
            if (resultText != null)
                resultText.text = "Jawaban Salah!";
        }
    }
}
