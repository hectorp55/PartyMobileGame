using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LoadMiniGames : MonoBehaviour
{
    public GameObject minigame_button_prefab;

    private const string PATH_TO_MINIGAMES = "Assets/Scripts/Mini-Games";

    void Awake() {
        var minigame_directories = Directory.GetDirectories(PATH_TO_MINIGAMES);
        foreach (string directory in minigame_directories) {
            GameObject new_minigame_button = (GameObject)Instantiate(minigame_button_prefab, transform);               
            string name = directory.Substring(PATH_TO_MINIGAMES.Length + 1);
            new_minigame_button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
    }  
}
