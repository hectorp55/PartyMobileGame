using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGame : MonoBehaviour
{
    public Text CountDownText;

    protected bool IsGameStarted;
    protected bool IsGameEnded;
    protected static float TimeLeft;

    void Start()
    {
        StartGame();
    }

    // Used to start the minigame
    public abstract void StartGame();

    // Used to report score to server when minigame is completed
    public void ReportScore(float score) {
        Debug.Log($"Score:  {score}");
        // Report score to server for playerid

    }

    public void EndGame() {
        // Return to lobby
    }

    #region Shared Protected Functions
    protected void UpdateBackgroundColor(Color color) {
        Camera.main.backgroundColor = color;
    }


    protected float CountDown(float currentTime) {
        var updatedTime = currentTime - Time.deltaTime;
        CountDownText.text = TimeLeft.ToString("F2");

        return updatedTime;
    }
    #endregion
}
