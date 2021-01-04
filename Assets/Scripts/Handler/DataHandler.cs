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

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    private void LoadData() {
        LoadPlayerProperties();
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

    private void Save() {
        PlayerPrefs.SetInt(PLAYER_CASH, playerCash);
        PlayerPrefs.SetInt(HIGH_SCORE, highScore);
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
}
