using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreObserver : MonoBehaviour {
    DataHandler data;

    private void Start() {
        data = DataHandler.instance;
        data.onResetHighScore += OnResetHighScore;
    }

    private void OnResetHighScore() {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = "0";
    }

    private void OnDestroy() {
        data.onResetHighScore += OnResetHighScore;
    }
}
