using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedText : MiniGame
{
    public TextMeshProUGUI SentenceTextObject; 
    public TMP_InputField CurrentSentenceInputField;
    public GameObject CurrentSentence; 

    private const string SentenceToType = "Easy";

    public void Awake() {
        TimeLeft = 10f;
        SentenceTextObject.text = SentenceToType;

        CurrentSentenceInputField.onValueChanged.AddListener(OnValueChange);
    }

    public void OnDestroy() {
        CurrentSentenceInputField.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChange(string text)
    {
        if (IsSentenceComplete(SentenceTextObject.text, text)) {
            GameOver();
        }
    }

    public override void StartGame()
    {   
        IsGameStarted = true;
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);

            if (TimeLeft < 0) {
                GameOver();
            }
        }
    }

    public override string Name {
        get {
            return MiniGameNames.SpeedText;
        }
    }

    private void GameOver() {
        IsGameEnded = true;
        EndGame(CalculateScore(TimeLeft), this);
    }

    private bool IsSentenceComplete(string sentenceToType, string currentSentence) {
        var istrue = string.Equals(sentenceToType, currentSentence, StringComparison.OrdinalIgnoreCase);
        print(sentenceToType.Length);
        print(currentSentence.Length);
        return istrue;
    }

    private float CalculateScore(float timeLeft) {
        return timeLeft;
    }
}
