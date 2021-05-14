using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuickDraw : MiniGame
{
    private static float TimeLeft = 3f;

    private const float ShootingOrientation = 0.5f;
    private const float ReadyOrientation = 0.9f;

    public bool IsGameStarted;
    public bool IsGameEnded;
    public Text CountDownText;
    public bool IsShooting;

    public override void StartGame()
    {
        StartCoroutine(StartWhenPlayerIsReady());
    }

    void Update() {
        if (IsGameStarted && !IsGameEnded) {
            UpdateBackgroundColor(Color.green);
            TimeLeft = CountDown(TimeLeft);
            CountDownText.text = TimeLeft.ToString("F2");

            if (IsPlayerShooting()) {
                UpdateBackgroundColor(Color.red);
                ReportScore(TimeLeft);
                IsGameEnded = true;
            }
        }
    }

    #region Private Functions
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

    private float CountDown(float currentTime) {
        var newTime = currentTime - Time.deltaTime;
        if (currentTime.ToString().Substring(0, 1) != newTime.ToString().Substring(0, 1)) {
            Handheld.Vibrate();
        }

        return newTime;
    }

    private bool IsPlayerReady() {
        // Make sure player has not moved from this position for atleast a second?
        if (Input.acceleration.y > ReadyOrientation) {
            return true;
        }
    
        return false;
    }

    private void UpdateBackgroundColor(Color color) {
        Camera.main.backgroundColor = color;
    }
    #endregion
}
