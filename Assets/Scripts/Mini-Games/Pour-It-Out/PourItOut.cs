using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourItOut : MiniGame
{
    public Slider PourCup;
    public Button DesiredAmountMarker;

    private const float FullCupAmount = 100f;
    private const float EmptyCupAmount = 0f;
    private const float MaxRandomAmount = 70f;
    private const float MinRandomAmount = 30f;
    private const float MinTiltToPour = 0.1f;
    private const float PourSpeed = 1f;
    private static float GoalAmountInCup;

    void Awake()
    {
        TimeLeft = 10f;
        PourCup.value = FullCupAmount;
        GoalAmountInCup = GetRandomGoalAmount();

        var cupTransform = PourCup.GetComponent<RectTransform>();
        DesiredAmountMarker.transform.position -= GetDesiredMarkerDistanceFromTop(cupTransform.rect.height, GoalAmountInCup);
    }

    public override void StartGame()
    {
        IsGameStarted = true;
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);

            PourCup.value = GetCupAmountAfterPour(PourCup.value);
            if (TimeLeft < 0) {
                ReportScore(CalculateScore(GoalAmountInCup, PourCup.value));
                IsGameEnded = true;
            }
        }
    }

    #region 
    private float GetCupAmountAfterPour(float currentAmountInCup) {
        var pourTilt = Input.acceleration.y + 1f;
        var normalizedPour = pourTilt / 2; 
        var pourAmount = normalizedPour < MinTiltToPour ? 0 : normalizedPour * PourSpeed;

        var amountLeftInCup = currentAmountInCup - pourAmount;
        if (amountLeftInCup > FullCupAmount) {
            return FullCupAmount;
        } else if (amountLeftInCup < EmptyCupAmount) {
            return EmptyCupAmount;
        } else {
            return amountLeftInCup;
        }
    }

    private float CalculateScore(float goalAmount, float finishedAmount) {
        return Mathf.Abs(goalAmount - finishedAmount);
    }

    private float GetRandomGoalAmount() {
        return Random.Range(MaxRandomAmount, MinRandomAmount);
    }

    private Vector3 GetDesiredMarkerDistanceFromTop(float heightOfSlider, float desiredAmount) {
        var desiredPercentageFromTop = 1 - (desiredAmount / FullCupAmount);

        return new Vector3( 0, heightOfSlider * desiredPercentageFromTop, 0);
    }
    #endregion
}
