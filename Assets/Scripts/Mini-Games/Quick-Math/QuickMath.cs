using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class QuickMath : MiniGame
{
    public TextMeshProUGUI NumbersToMathTextObject;
    public TMP_InputField CurrentAnswerInputField;

    private static readonly string[] MathOperations = { "+", "-" };
    private static readonly float[] MathNumberRange = { 0f, 10f };
    private const float DoMathInterval = 3f;
    private const int MathCounts = 5;
    private float CorrectAnswer = 0f;
    private float MyAnswer = 0f;
    private List<float> MathsNumbers;

    public void Awake() {
        TimeLeft = 10f;
        CurrentAnswerInputField.onValueChanged.AddListener(OnValueChange);

        MathsNumbers = GenerateMathsArray(MathCounts);
        CorrectAnswer = MathsNumbers.Sum();
    }

    public void OnDestroy() {
        CurrentAnswerInputField.onValueChanged.RemoveAllListeners();
    }

    public override void StartGame() {   
        StartCoroutine(ShowMathNumbers());
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);

            if (TimeLeft < 0) {
                IsGameEnded = true;
                EndGame(CalculateScore(TimeLeft, CorrectAnswer, MyAnswer), this);
            }
        }
    }

    public override string Name {
        get {
            return MiniGameNames.QuickMath;
        }
    }

    private void OnValueChange(string text) {
        MyAnswer = float.Parse(text);
    }

    private float CalculateScore(float timeLeft, float correctAnswer, float myAnswer) {
        print(correctAnswer - myAnswer);
        return correctAnswer - myAnswer;
    }

    private List<float> GenerateMathsArray(int mathCount) {
        var mathsArray = new List<float>();
        
        for (int i = 0; i < mathCount; i++) {
            mathsArray.Add(Mathf.Round(Random.Range(MathNumberRange[0], MathNumberRange[1])));
        }

        return mathsArray;
    }

    private IEnumerator ShowMathNumbers() {
        var index = 0;
        while(index < MathCounts)
        {
            ShowNextMathNumber(index);
            index++;
            yield return new WaitForSeconds(DoMathInterval);
        }
        NumbersToMathTextObject.text = "GO!";
        IsGameStarted = true;
    }

    private void ShowNextMathNumber(int currentMathIndex) {
        NumbersToMathTextObject.text = MathsNumbers[currentMathIndex].ToString();
    }
}
