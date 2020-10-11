using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {
    public static DataHandler instance;

    //Player Properties
    string PLAYER_CASH = "PlayerCash";
    public int playerCash;

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
        } else {
            playerCash = 0;
        }
    }

    private void Save() {
        PlayerPrefs.SetInt(PLAYER_CASH, playerCash);
    }

    public void IncreaseCash(int amount) {
        playerCash += amount;
        Save();
    }

    public void DecreaseCash(int amount) {
        playerCash -= amount;
        Save();
    }
}
