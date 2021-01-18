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
    public GameObject gameOverMenu;

    public TextMeshProUGUI mainMenuCash;
    public TextMeshProUGUI mainMenuHighScore;

    public GameObject scorePanel;

    public TextMeshProUGUI recordText;
    public TextMeshProUGUI newRecordText;
    public TextMeshProUGUI recordCounter;

    public GameObject achievementTitle;
    public GameObject achievementDescription;
    public GameObject achievementBackground;

    public int achievementMessageTime = 3;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        ReloadHandler.instance.onClickPlay += HideMenus;
        ReloadHandler.instance.onClickRetry += HideMenus;
        PlayerMovementHandler.instance.onPlayerDie += ShowEnding;
        mainMenuCash.text = DataHandler.instance.playerCash.ToString();
        mainMenuHighScore.text = DataHandler.instance.highScore.ToString();
    }

    private void HideMenus() {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scorePanel.SetActive(true);
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
        recordCounter.text = DataHandler.instance.highScore.ToString();
        scorePanel.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ShowAchievement(Achievement achievement) {
        StartCoroutine(ShowAchievementMessage(achievement));
    }

    IEnumerator ShowAchievementMessage(Achievement achievement) {
        achievementTitle.GetComponent<TextMeshProUGUI>().text = LocalizationSystem.GetLocalizedValue(achievement.achievementTitle);
        achievementTitle.SetActive(true);
        achievementDescription.GetComponent<TextMeshProUGUI>().text = LocalizationSystem.GetLocalizedValue(achievement.achievmentDescription);
        achievementDescription.SetActive(true);
        achievementBackground.SetActive(true);
        yield return new WaitForSeconds(achievementMessageTime);
        achievementTitle.SetActive(false);
        achievementDescription.SetActive(false);
        achievementBackground.SetActive(false);
    }

    private void OnDestroy() {
        ReloadHandler.instance.onClickPlay -= HideMenus;
        ReloadHandler.instance.onClickRetry -= HideMenus;
        PlayerMovementHandler.instance.onPlayerDie -= ShowEnding;
    }

    public void UpdateCash(int currentScore) {
        currentScoreText.text = currentScore.ToString();
    }
}
