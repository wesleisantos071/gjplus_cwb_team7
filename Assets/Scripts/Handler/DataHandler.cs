using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataHandler : MonoBehaviour {
    public static DataHandler instance;

    //Player Properties
    string KEY_PLAYER_CASH = "PlayerCash";
    public int playerCash = 0;
    string KEY_FIRE_RECORD = "PlayerFireRecord";
    public int fireRecord = 0;
    string KEY_DISTANCE_RECORD = "PlayerDistanceRecord";
    public int distanceRecord = 0;

    string KEY_MUSIC = "MusicEnabled";
    public bool musicEnabled = true;
    string KEY_SOUND = "SoundEnabled";
    public bool soundEnabled = true;
    string KEY_LANG = "Language";
    public LocalizationSystem.Language selectedLanguage;
    string KEY_ACHIEVEMENT_FIRE_INDEX = "AchievementFireIndex";
    public int fireAchievementIndex = 0;
    string KEY_ACHIEVEMENT_DISTANCE_INDEX = "AchievementDistanceIndex";
    public int distanceAchievementIndex = 0;

    public Action onResetHighScores;

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
        if (PlayerPrefs.HasKey(KEY_PLAYER_CASH)) {
            playerCash = PlayerPrefs.GetInt(KEY_PLAYER_CASH);
            fireRecord = PlayerPrefs.GetInt(KEY_FIRE_RECORD);
            distanceRecord = PlayerPrefs.GetInt(KEY_DISTANCE_RECORD);
        }
    }

    private void LoadSettings() {
        selectedLanguage = LocalizationSystem.Language.English;
        string langSaved = PlayerPrefs.GetString(KEY_LANG, null);
        if (langSaved != null) {
            foreach (LocalizationSystem.Language lang in Enum.GetValues(typeof(LocalizationSystem.Language))) {
                if (lang.ToString().Equals(langSaved)) {
                    selectedLanguage = lang;
                    break;
                }
            }
        }
        if (PlayerPrefs.HasKey(KEY_MUSIC)) {
            musicEnabled = PlayerPrefs.GetInt(KEY_MUSIC) == 1 ? true : false;
            soundEnabled = PlayerPrefs.GetInt(KEY_SOUND) == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey(KEY_ACHIEVEMENT_FIRE_INDEX)) {
            fireAchievementIndex = PlayerPrefs.GetInt(KEY_ACHIEVEMENT_FIRE_INDEX);
        }
        if (PlayerPrefs.HasKey(KEY_ACHIEVEMENT_DISTANCE_INDEX)) {
            distanceAchievementIndex = PlayerPrefs.GetInt(KEY_ACHIEVEMENT_DISTANCE_INDEX);
        }
    }

    private void Save() {
        PlayerPrefs.SetInt(KEY_PLAYER_CASH, playerCash);
        PlayerPrefs.SetInt(KEY_FIRE_RECORD, fireRecord);
        PlayerPrefs.SetInt(KEY_DISTANCE_RECORD, distanceRecord);
        PlayerPrefs.SetString(KEY_LANG, selectedLanguage.ToString());
        PlayerPrefs.SetInt(KEY_MUSIC, musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt(KEY_SOUND, soundEnabled ? 1 : 0);
        PlayerPrefs.SetInt(KEY_ACHIEVEMENT_FIRE_INDEX, fireAchievementIndex);
        PlayerPrefs.SetInt(KEY_ACHIEVEMENT_DISTANCE_INDEX, distanceAchievementIndex);
    }

    internal void SetFireAchievementIndex(int newVal) {
        fireAchievementIndex = newVal;
        Save();
    }

    internal void SetDistanceAchievementIndex(int newVal) {
        distanceAchievementIndex = newVal;
        Save();
    }

    public void IncreaseCash(int amount) {
        playerCash += amount;
        Save();
    }

    public void DecreaseCash(int amount) {
        playerCash -= amount;
        Save();
    }

    /**
     * return true if new highscore, false if not
     * 
     */
    public bool SaveFireRecord(int newScore) {
        if (fireRecord < newScore) {
            fireRecord = newScore;
            Save();
            return true;
        }
        return false;
    }

    public void ResetHighScores() {
        fireRecord = 0;
        fireAchievementIndex = 0;
        distanceAchievementIndex = 0;
        distanceRecord = 0;
        playerCash = 0;
        Save();
        onResetHighScores?.Invoke();
    }

    /**
     * return true if new playerFireRecord, false if not
     * 
     */
    public bool SaveDistanceRecord(int newDistance) {
        if (distanceRecord < newDistance) {
            distanceRecord = newDistance;
            Save();
            return true;
        }
        return false;
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
