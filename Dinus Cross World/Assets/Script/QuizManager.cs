using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public CheckBoxController[] questions;
    public GameObject paperUI;
    public GameObject textUI;
    public GameObject paperObject;

    public TaskManager taskManager;

    private bool[] answers;

    void Start()
    {
        answers = new bool[questions.Length];

        for (int i = 0; i < questions.Length; i++)
        {
            int index = i;
            questions[i].OnAnswered.AddListener((isCorrect) => HandleAnswer(index, isCorrect));
        }
    }

    void HandleAnswer(int questionIndex, bool isCorrect)
    {
        answers[questionIndex] = isCorrect;

        Debug.Log($"Question {questionIndex + 1} answered: {(isCorrect ? "Benar" : "Salah")}");

        if (AllAnswered())
        {
            int correctCount = 0;
            foreach (bool ans in answers)
                if (ans) correctCount++;

            Debug.Log($"Total Benar: {correctCount} dari {answers.Length}");

            paperUI.SetActive(false); // Tutup UI kertas

            if (correctCount == answers.Length)
            {
                paperObject.SetActive(false); // Semua benar
                taskManager?.CompleteTask();
            }
            else
            {
                textUI.SetActive(true); // Ada yang salah
            }

            // Reset setelah hasil ditampilkan
            Invoke(nameof(ResetQuiz), 1.5f); // Reset setelah 1.5 detik
        }
    }

    bool AllAnswered()
    {
        foreach (var controller in questions)
        {
            if (!controller.HasAnswered)
                return false;
        }
        return true;
    }

    void ResetQuiz()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].ResetAnswer();
            answers[i] = false;
        }

        textUI.SetActive(false);
    }
}

