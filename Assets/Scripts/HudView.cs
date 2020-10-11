using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudView : MonoBehaviour {
    public static HudView instance;
    public TextMeshProUGUI currentScoreText;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void UpdateCash(int currentScore) {
        currentScoreText.text = currentScore.ToString();
    }
}
