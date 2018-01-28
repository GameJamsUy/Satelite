using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public SoundScript soundRef;

    public string nextSceneToLoad;
    
    public void LoadNextScene() {
        SceneManager.LoadScene(nextSceneToLoad, LoadSceneMode.Single);
    }

    public void GoToStartMenu() {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

    public void RestartScene() {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
