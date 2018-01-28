using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighliter : MonoBehaviour {

    SpriteRenderer buttonSprite;

    public Color normalColor;
    public Color highlightedColor;

	// Use this for initialization
	void Start () {
        buttonSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

    void OnMouseEnter() {
        buttonSprite.color = highlightedColor;
    }


    void OnMouseExit() {
        buttonSprite.color = normalColor;
    }
}
