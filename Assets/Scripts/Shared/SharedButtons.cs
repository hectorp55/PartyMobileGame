using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedButtons : MonoBehaviour
{
    public void BackToTitleScreen() {
        SceneManager.LoadScene(SceneManager.TitleMenu);
    }
    
}
