using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizer : MonoBehaviour {
    TextMeshProUGUI textField;
    public string key;
    void Start() {
        textField = GetComponent<TextMeshProUGUI>();
        UpdateLanguage();
        LocalizationSystem.onChangeLanguage += UpdateLanguage;
    }

    public void OnChangeLanguage(int newLanguage) {
        LocalizationSystem.AlternateLanguage(newLanguage);
    }

    private void UpdateLanguage() {
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
    }

    private void OnDestroy() {
        LocalizationSystem.onChangeLanguage -= UpdateLanguage;
    }
}

