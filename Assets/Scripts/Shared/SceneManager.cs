using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    // ===========================================
    // Scene Names
    public const string TitleMenu = "Title";
    public const string HostGameMenu = "Host";
    public const string JoinGameMenu = "Join";
    // ===========================================

    public static void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
