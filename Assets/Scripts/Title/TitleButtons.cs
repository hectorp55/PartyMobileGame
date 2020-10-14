using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    public void OpenHostGameMenu() {
        SceneManager.LoadScene(SceneManager.HostGameMenu);
    }

    public void OpenJoinGameMenu() {
        SceneManager.LoadScene(SceneManager.JoinGameMenu);
    }
}
