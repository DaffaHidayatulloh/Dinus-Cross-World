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
    public GameObject npcPakHamadi;


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
                AudioManager.instance.PlaySFX(5);
                paperObject.SetActive(false); // Semua benar
                taskManager?.CompleteTask();
            }
            else
            {
                AudioManager.instance.PlaySFX(10);
                textUI.SetActive(true); // Ada yang salah
                StartCoroutine(ShakeTextUI());

                if (npcPakHamadi != null)
                {
                    SpriteRenderer sprite = npcPakHamadi.GetComponent<SpriteRenderer>();
                    if (sprite != null)
                        StartCoroutine(FlipSpriteTemporarily(sprite));
                }

            }

            // Reset setelah hasil ditampilkan
            Invoke(nameof(ResetQuiz), 1f); // Reset setelah 1.5 detik
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
    IEnumerator ShakeTextUI(float duration = 0.3f, float magnitude = 5f)
    {
        Vector3 originalPos = textUI.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            textUI.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        textUI.transform.localPosition = originalPos;
    }
    IEnumerator FlipSpriteTemporarily(SpriteRenderer sprite, float duration = 1.5f)
    {
        sprite.flipX = true;
        yield return new WaitForSeconds(duration);
        sprite.flipX = false;
    }

}

