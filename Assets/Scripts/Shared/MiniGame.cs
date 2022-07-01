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

    public void EndGame(float score, MiniGame minigameName) {
        GameManager.instance.EndGame(score, this);
        // Return to lobby
    }

    public abstract string Name {
        get;
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
