using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    public Text displayText;
    public InputField answerInput;
    public Slider timeSlider;
    public GameObject arrowIndicator;
    private string correctAnswer = "Jakarta";
    private Queue<string> dialogLines = new Queue<string>();
    private float timeLeft = 10f;
    private bool isWaitingForInput = false;
    private float delayBeforeNextLine = 0.25f;
    private float timer = 0f;

    void Start()
    {
        SetupDialog();
        StartDialog();
        answerInput.gameObject.SetActive(false);
        timeSlider.gameObject.SetActive(false);
        arrowIndicator.SetActive(false);
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

    void Update()
    {
        if (isWaitingForInput && timer <= 0)
        {
            arrowIndicator.SetActive(true); // Tampilkan panah saat pemain dapat menekan Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                arrowIndicator.SetActive(false); // Nonaktifkan panah segera setelah Space ditekan
                ContinueDialog();
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            arrowIndicator.SetActive(false); // Sembunyikan panah selama hitungan mundur timer
        }
    }

    void StartDialog()
    {
        if (dialogLines.Count > 0)
        {
            Debug.Log("StartDialog called. Remaining dialog lines: " + dialogLines.Count);
            string line = dialogLines.Dequeue();
            Debug.Log("Processing line: " + line);
            StartCoroutine(TypeSentence(line));
        }
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        displayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        // If this is the last line, automatically continue after delay
        if (dialogLines.Count == 0)
        {
            yield return new WaitForSeconds(delayBeforeNextLine);
            EnableInput();
            StartTimer();
        }
        else
        {
            yield return new WaitForSeconds(delayBeforeNextLine);
            isWaitingForInput = true;
            timer = delayBeforeNextLine;
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
            CompleteAnswering();
        }
        else
        {
            Debug.Log("Jawaban Salah, coba lagi.");
            // Jangan deactivate input field; biarkan pemain terus mencoba
            answerInput.text = ""; // Bersihkan field untuk jawaban baru
            answerInput.ActivateInputField();
            answerInput.Select();
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

    void StartTimer()
    {
        timeSlider.gameObject.SetActive(true);
        timeSlider.maxValue = timeLeft;
        timeSlider.value = timeLeft;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeSlider.value = timeLeft;
            yield return null;
        }
        TimeOut();
    }

    void TimeOut() 
    {
        Debug.Log("Time is up");
        CompleteAnswering();
    }

    void ContinueDialog()
    {
        if (dialogLines.Count > 0)
        {
            string line = dialogLines.Dequeue();
            StartCoroutine(TypeSentence(line));
        }
        else
        {
            EnableInput();
            StartTimer();
        }
        isWaitingForInput = false;
    }

    private void CompleteAnswering()
    {
        answerInput.DeactivateInputField();
        answerInput.onValueChanged.RemoveAllListeners();
        answerInput.onEndEdit.RemoveAllListeners();
        StopAllCoroutines(); // Stop the timer
        timeSlider.gameObject.SetActive(false); // Hide the timer slider
    }
}
