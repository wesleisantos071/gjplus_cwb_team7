using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicToggleHandler : MonoBehaviour {
    private string textOn = "ON";
    private string textOff = "OFF";

    DataHandler data;
    TextMeshProUGUI buttonText;

    private void Start() {
        data = DataHandler.instance;
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = data.musicEnabled ? textOn : textOff;
    }

    public void OnToggleMusic() {
        data.SetMusicEnabled(!data.musicEnabled);
        buttonText.text = data.musicEnabled ? textOn : textOff;
    }
}
