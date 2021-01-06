using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudView : MonoBehaviour {
    public static HudView instance;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI gameoverScoreText;
    public GameObject mainMenu;
    public GameObject endScreen;

    public TextMeshProUGUI mainMenuCash;
    public TextMeshProUGUI mainMenuHighScore;

    public TextMeshProUGUI recordText;
    public TextMeshProUGUI newRecordText;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        ReloadHandler.instance.onClickPlay += HideMainMenu;
        PlayerMovementHandler.instance.onPlayerDie += ShowEnding;
        mainMenuCash.text = DataHandler.instance.playerCash.ToString();
        mainMenuHighScore.text = DataHandler.instance.highScore.ToString();
    }

    private void HideMainMenu() {
        mainMenu.SetActive(false);
    }

    void ShowEnding() {
        int currentScore = Convert.ToInt32(currentScoreText.text);
        DataHandler.instance.IncreaseCash(currentScore);
        bool newHighScore = DataHandler.instance.SaveHighScore(currentScore);
        if (newHighScore) {
            recordText.enabled = false;
            newRecordText.enabled = true;
        } else {
            recordText.enabled = true;
            newRecordText.enabled = false;
        }
        StartCoroutine(DelayedGameover());
    }

    IEnumerator DelayedGameover() {
        yield return new WaitForSeconds(2);
        gameoverScoreText.text = currentScoreText.text;
        endScreen.SetActive(true);
    }

    private void OnDestroy() {
        ReloadHandler.instance.onClickPlay -= HideMainMenu;
        PlayerMovementHandler.instance.onPlayerDie -= ShowEnding;
    }

    public void UpdateCash(int currentScore) {
        currentScoreText.text = currentScore.ToString();
    }
}
