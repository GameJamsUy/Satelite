using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour {

    public void RestartButton(){
        SceneManager.LoadScene("main");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("StartScreen");
    }
}
