using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeIt : MiniGame
{
    public Text ScoreText;

    private float Score = 0f;
    private bool ShouldPlayerBeShaking = false;
    private float NextShakeStateSwitch = 30f;
    private const float ShakeStateSwitchInterval = 5f;
    private const float MinimumShakeRequired = 0.1f;

    void Awake() {
        TimeLeft = 30f;
    }

    public override void StartGame()
    {
        IsGameStarted = true;
        ScoreText.text = Score.ToString("F2");
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);
            AlternateBackgroundColors(TimeLeft);

            if (IsShakeing(out var shakeSpeed)) {
                Score += CalculateScore(shakeSpeed, ShouldPlayerBeShaking);
                ScoreText.text = Score.ToString("F2");
            }

            if (TimeLeft < 0) {
                IsGameEnded = true;
                EndGame(Score, this);
            }
        }
    }

    public override string Name {
        get {
            return MiniGameNames.ShakeIt;
        }
    }

    #region Private Members
    private void AlternateBackgroundColors(float currentTime) {
        if (currentTime < NextShakeStateSwitch) {
            NextShakeStateSwitch = currentTime - ShakeStateSwitchInterval;
            ShouldPlayerBeShaking = InvertShakeState(ShouldPlayerBeShaking);
            Handheld.Vibrate();
        }
    }

    private bool InvertShakeState(bool currentShakeState) {
        var nextShakeState = !currentShakeState;
        var nextBackgroundColor = nextShakeState ? Color.green : Color.red;
        UpdateBackgroundColor(nextBackgroundColor);

        return nextShakeState;
    }

    private bool IsShakeing(out float shakeSpeed) {
        shakeSpeed = Input.gyro.rotationRateUnbiased.magnitude;
        if (shakeSpeed > MinimumShakeRequired) {
            return true;
        }

        return false;
    }

    private float CalculateScore(float shakeSpeed, bool shouldPlayerBeShaking) {
        return shakeSpeed * (shouldPlayerBeShaking ? 1 : -1);
    }
    #endregion
}
