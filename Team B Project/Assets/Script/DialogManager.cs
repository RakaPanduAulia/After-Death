using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    public Text displayText;
    public InputField answerInput;
    private string correctAnswer = "Jakarta";
    private Queue<string> dialogLines = new Queue<string>();

    void Start()
    {
        SetupDialog();
        StartDialog();
        answerInput.gameObject.SetActive(false);
        answerInput.onValueChanged.AddListener(delegate { ValidateText(answerInput.text); });
        answerInput.onEndEdit.AddListener(delegate { AttemptSubmit(answerInput); });
    }

    void SetupDialog()
    {
        dialogLines.Enqueue("Selamat datang di permainan kita!");
        dialogLines.Enqueue("Hari ini kita akan menguji pengetahuanmu.");
        dialogLines.Enqueue("Siap untuk pertanyaan pertama?");
        dialogLines.Enqueue("Apa ibukota dari Indonesia?");
    }

    void StartDialog()
    {
        Debug.Log("StartDialog called. Remaining dialog lines: " + dialogLines.Count);
        string line = dialogLines.Dequeue();
        Debug.Log("Processing line: " + line);
        // StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(string sentence)
    {
        displayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        
        // Jika dialog terakhir adalah pertanyaan, tunggu jawaban.
        if (dialogLines.Count == 0)
        {
            Debug.Log("Finished all dialogs, enabling input field...");
            correctAnswer = "Jakarta"; // Setelah semua dialog selesai.
            EnableInput();
        }
        else
        {
            Debug.Log("There are more dialogs to process.");
            yield return new WaitForSeconds(1); // Tunggu sebentar sebelum dialog berikutnya.
            StartDialog();
        }
    }

    void EnableInput()
    {
        Debug.Log("Enabling input field...");
        answerInput.gameObject.SetActive(true);
        answerInput.Select();
    }

    public void CheckAnswer()
    {
        if (answerInput.text.Trim().ToLower() == correctAnswer.ToLower())
        {
            Debug.Log("Jawaban Benar!");
            answerInput.DeactivateInputField();
            answerInput.onValueChanged.RemoveAllListeners();
            answerInput.onEndEdit.RemoveAllListeners();
        }
        else
        {
            Debug.Log("Jawaban Salah, coba lagi.");
            answerInput.ActivateInputField();
        }
    }

    private void AttemptSubmit(InputField input)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckAnswer();
            input.text = ""; // Opsional: Bersihkan field setelah submit
        }
    }

    void ValidateText(string inputText)
    {
        if (!string.IsNullOrWhiteSpace(inputText) && !correctAnswer.StartsWith(inputText, StringComparison.CurrentCultureIgnoreCase))
        {
            // Teks berwarna merah jika ada kesalahan
            answerInput.textComponent.color = Color.red;
        }
        else
        {
            // Teks berwarna hitam jika benar
            answerInput.textComponent.color = Color.black;
        }
    }
}
