using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeGameManager : GameManager
{
    public override void EndGame(float score, MiniGame minigame) {
        this.minigame = minigame;
        HighScores.ReportCurrentScore(score, minigame.Name);

        SceneManager.LoadScene(SceneManager.HighScoreMenu);
    }
}
