using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
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
}
