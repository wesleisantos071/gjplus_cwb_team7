using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataHandler : MonoBehaviour {
    public static DataHandler instance;

    //Player Properties
    string PLAYER_CASH = "PlayerCash";
    public int playerCash;
    string HIGH_SCORE = "HighScore";
    public int highScore;

    public bool musicEnabled;
    public bool soundEnabled;
    public LocalizationSystem.Language selectedLanguage;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    private void LoadData() {
        LoadPlayerProperties();
        LoadSettings();
    }

    private void LoadPlayerProperties() {
        if (PlayerPrefs.HasKey(PLAYER_CASH)) {
            playerCash = PlayerPrefs.GetInt(PLAYER_CASH);
            highScore = PlayerPrefs.GetInt(HIGH_SCORE);
        } else {
            playerCash = 0;
            highScore = 0;
        }
    }

    private void LoadSettings() {
        selectedLanguage = LocalizationSystem.Language.English;
        string langSaved = PlayerPrefs.GetString("Language", null);
        if (langSaved != null) {
            foreach (LocalizationSystem.Language lang in Enum.GetValues(typeof(LocalizationSystem.Language))) {
                if (lang.ToString().Equals(langSaved)) {
                    selectedLanguage = lang;
                    break;
                }
            }
        }
        if (PlayerPrefs.HasKey("MusicEnabled")) {
            musicEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1 ? true : false;
            soundEnabled = PlayerPrefs.GetInt("SoundEnabled") == 1 ? true : false;
        } else {
            musicEnabled = true;
            soundEnabled = true;
            Save();
        }
    }

    private void Save() {
        PlayerPrefs.SetInt(PLAYER_CASH, playerCash);
        PlayerPrefs.SetInt(HIGH_SCORE, highScore);
        PlayerPrefs.SetString("Language", selectedLanguage.ToString());
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
    }

    public void IncreaseCash(int amount) {
        playerCash += amount;
        Save();
    }

    public void DecreaseCash(int amount) {
        playerCash -= amount;
        Save();
    }

    internal void SaveHighScore(int newScore) {
        if (highScore < newScore) {
            highScore = newScore;
        }
        Save();
    }

    public void SetSelectedLanguage(LocalizationSystem.Language language) {
        selectedLanguage = language;
        Save();
    }

    public void SetMusicEnabled(bool isEnabled) {
        musicEnabled = isEnabled;
        if (musicEnabled) {
            AudioHandler.instance.ResumeMusic();
        } else {
            AudioHandler.instance.StopMusic();
        }
        Save();
    }

    public void SetSoundEnabled(bool isEnabled) {
        soundEnabled = isEnabled;
        Save();
    }
}
