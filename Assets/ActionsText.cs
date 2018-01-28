﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsText : MonoBehaviour {
    public MainLevelScript msc;
    private Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "ACTIONS: " + msc.GetCurrentMovements().ToString();
    }
}
