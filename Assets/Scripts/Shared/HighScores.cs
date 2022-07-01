using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class HighScores
{
    private const int MAX_NUMBER_OF_HIGHSCORES = 6;
    private const string PLAYER_PREF_KEY = "HighScores";

    public Dictionary<string, List<float>> GameScores;

    public HighScores() {
        GameScores = new Dictionary<string, List<float>>();
    }

    public static List<float> GetHighScoresByGameName(string gameName) {
        HighScores currentScores = GetHighScoresFromPlayerPrefs();

        List<float> gameScores;
        if (currentScores.GameScores.TryGetValue(gameName, out gameScores)) {
            return gameScores;
        } else {
            return new List<float>();
        }
    }

    public static void ReportCurrentScore(float currentScore, string minigameName) {
        HighScores currentScores = GetHighScoresFromPlayerPrefs();

        List<float> gameScores;
        if (currentScores.GameScores.TryGetValue(minigameName, out gameScores)) {
            currentScores.UpdateHighScores(gameScores, currentScore);
        } else {
            currentScores.GameScores.Add(minigameName, new List<float> { currentScore });
        }

        currentScores.SaveHighScoresToPlayerPrefs();
    }

    private static HighScores GetHighScoresFromPlayerPrefs() {
        string highScoreJson = PlayerPrefs.GetString(PLAYER_PREF_KEY, null);

        if(String.IsNullOrEmpty(highScoreJson)) {
            return new HighScores();
        } else {
            return JsonConvert.DeserializeObject<HighScores>(highScoreJson); 
        }
    }

    private void SaveHighScoresToPlayerPrefs() {
        string currentHighScoresJson = JsonConvert.SerializeObject(this);
        PlayerPrefs.SetString(PLAYER_PREF_KEY, currentHighScoresJson);
    }

    private List<float> UpdateHighScores(List<float> gameScores, float currentScore) {
        if (currentScore > gameScores[gameScores.Count - 1]) {
            int index = 0;
            while (index < gameScores.Count - 1 && gameScores[index] > currentScore) {
                index++;
            }
            gameScores.Insert(index, currentScore);
            if (gameScores.Count >= MAX_NUMBER_OF_HIGHSCORES) {
                gameScores.RemoveAt(gameScores.Count - 1);
            }
        }

        return gameScores;
    }
}
