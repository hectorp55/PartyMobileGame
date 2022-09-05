using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;

public abstract class GameManager : MonoBehaviour
{
    private const string MINIGAME_TAG = "MiniGame";
    
    protected MiniGame minigame;

    public static GameManager instance = null;

    public abstract void EndGame(float score, MiniGame minigame);

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public string GetCurrentMinigameName() {
        return minigame.Name;
    }
}
