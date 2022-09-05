using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LoadMiniGames : MonoBehaviour
{
    public GameObject minigame_button_prefab;

    private const string PATH_TO_MINIGAMES = "Assets/Scenes/Mini-Games";
    private const string SCENE_EXTENSION = "*.unity";

    void Awake() {
        string [] minigame_paths = Directory.GetFiles(PATH_TO_MINIGAMES, SCENE_EXTENSION);
        foreach (string minigame_path in minigame_paths) {
            GameObject new_minigame_button = (GameObject)Instantiate(minigame_button_prefab, transform);       
            
            var minigame = Path.GetFileNameWithoutExtension(minigame_path);

            new_minigame_button.GetComponentInChildren<TextMeshProUGUI>().text = minigame;
            new_minigame_button.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(minigame); });
        }
    }  
}
