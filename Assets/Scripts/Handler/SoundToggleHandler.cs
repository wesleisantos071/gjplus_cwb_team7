using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundToggleHandler : MonoBehaviour {
    private string textOn = "ON";
    private string textOff = "OFF";

    DataHandler data;
    TextMeshProUGUI buttonText;

    private void Start() {
        data = DataHandler.instance;
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = data.soundEnabled ? textOn : textOff;
    }

    public void OnToggleSound() {
        data.SetSoundEnabled(!data.soundEnabled);
        buttonText.text = data.soundEnabled ? textOn : textOff;
    }

}
