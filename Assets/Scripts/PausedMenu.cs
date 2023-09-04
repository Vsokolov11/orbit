using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public void ResumeGame() {
        SceneManager.LoadScene(1);
    }

    public void LoadGame() {
        
    }

    public void ExitGame() {
        Application.Quit();
    }
}
