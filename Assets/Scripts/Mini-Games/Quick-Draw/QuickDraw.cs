using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDraw : MiniGame
{
    public bool IsShooting;

    void Awake() {
        TimeLeft = 3f;
    }

    public override void StartGame()
    {
        StartCoroutine(StartWhenPlayerIsReady());
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            UpdateBackgroundColor(Color.green);
            
            var oldTime = TimeLeft;
            TimeLeft = CountDown(TimeLeft);
            if (TimeLeft.ToString().Substring(0, 1) != oldTime.ToString().Substring(0, 1)) {
                Handheld.Vibrate();
            }
            CountDownText.text = TimeLeft.ToString("F2");

            if (IsPlayerShooting()) {
                UpdateBackgroundColor(Color.red);
                ReportScore(TimeLeft);
                IsGameEnded = true;
            }
        }
    }

    #region Private Members
    private const float ShootingOrientation = 0.5f;
    private const float ReadyOrientation = 0.9f;

    private IEnumerator StartWhenPlayerIsReady() { 
        yield return new WaitUntil(() => IsPlayerReady() == true); 

        IsGameStarted = true;
    }

    private bool IsPlayerShooting() {
        if (Input.acceleration.y < ShootingOrientation) {
            return true;
        }

        return false;
    }

    private bool IsPlayerReady() {
        // Make sure player has not moved from this position for atleast a second?
        if (Input.acceleration.y > ReadyOrientation) {
            return true;
        }
    
        return false;
    }
    #endregion
}
