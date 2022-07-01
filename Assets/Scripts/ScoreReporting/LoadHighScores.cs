using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadHighScores : MonoBehaviour
{
    public GameObject highscore_text_prefab;

    void Awake() {
        List<float> currentGameHighScore = HighScores.GetHighScoresByGameName(GameManager.instance.GetCurrentMinigameName());
        int index = 1;
        foreach (float score in currentGameHighScore) {
            GameObject new_score_count_text = (GameObject)Instantiate(highscore_text_prefab, transform);
            new_score_count_text.GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();

            GameObject new_highscore_text = (GameObject)Instantiate(highscore_text_prefab, transform);       
            new_highscore_text.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
            new_highscore_text.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;

            index++;
        }
    }
}
