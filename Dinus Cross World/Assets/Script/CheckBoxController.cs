using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckBoxController : MonoBehaviour
{
    [System.Serializable]
    public class AnswerButton
    {
        public Button button;
        public GameObject checkmarkOverlay;
    }

    public AnswerButton[] answerButtons;
    public int correctIndex = 0;
    public Text resultText;

    public UnityEvent<bool> OnAnswered; // Event untuk memberitahu hasil jawaban

    private bool hasAnswered = false;

    void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].button.onClick.AddListener(() => OnButtonClicked(index));
            answerButtons[i].checkmarkOverlay.SetActive(false);
        }
    }

    void OnButtonClicked(int index)
    {
        if (hasAnswered) return; // Hanya bisa menjawab sekali
        hasAnswered = true;

        foreach (var ab in answerButtons)
        {
            ab.checkmarkOverlay.SetActive(false);
        }

        answerButtons[index].checkmarkOverlay.SetActive(true);

        bool isCorrect = index == correctIndex;

        if (resultText != null)
            resultText.text = isCorrect ? "Jawaban Benar!" : "Jawaban Salah!";

        OnAnswered.Invoke(isCorrect);
    }
}
