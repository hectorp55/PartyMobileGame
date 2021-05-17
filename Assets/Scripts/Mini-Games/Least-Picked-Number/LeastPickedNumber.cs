using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeastPickedNumber : MiniGame
{
    public Slider NumberSlider;
    public Text SelectedNumber;

    void Awake() {
        TimeLeft = 10f;
    }

    public override void StartGame()
    {
        IsGameStarted = true;
        NumberSlider.maxValue = GetSliderMaxNumber();
        UpdateSelectedNumber();
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);

            if (TimeLeft < 0) {
                ReportScore(NumberSlider.value);
                IsGameEnded = true;
            }
        }
    }

    public void UpdateSelectedNumber() {
        SelectedNumber.text = NumberSlider.value.ToString();
    }

    #region 
    private int GetSliderMaxNumber() {
        // TODO: Max number should be number of players - 1
        return 20; // players.count - 1;
    }
    #endregion
}
