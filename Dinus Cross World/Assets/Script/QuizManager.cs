using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public CheckBoxController[] questions; // Assign semua CheckBoxController di Inspector

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

        // Cek apakah semua sudah terjawab
        if (AllAnswered())
        {
            int correctCount = 0;
            foreach (bool ans in answers)
                if (ans) correctCount++;

            Debug.Log($"Total Benar: {correctCount} dari {answers.Length}");
        }
    }

    bool AllAnswered()
    {
        foreach (var controller in questions)
        {
            // Gunakan refleksi internal atau flag jika butuh pengecekan tambahan
            // Misalnya cek `hasAnswered` bila public, atau jumlah OnAnswered yang telah terpanggil
        }
        return true; // Optional: bisa buat validasi yang lebih ketat
    }
}
