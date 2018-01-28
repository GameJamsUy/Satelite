using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAssigner : MonoBehaviour {
    
    public enum ButtonType {Restart, Next, MainMenu}
    public ButtonType thisButtonType;

    // Use this for initialization
    void Start () {
        SceneSwitcher sceneSwitcher = transform.root.GetComponent<SceneSwitcher>();
        Button button = transform.GetComponent<Button>();

        switch (thisButtonType) {
            case ButtonType.Restart:
                button.onClick.AddListener(sceneSwitcher.RestartScene);
                break;
            case ButtonType.Next:
                button.onClick.AddListener(sceneSwitcher.LoadNextScene);
                break;
            case ButtonType.MainMenu:
                button.onClick.AddListener(sceneSwitcher.GoToStartMenu);
                break;
            default:
                break;
        }
    }

}
