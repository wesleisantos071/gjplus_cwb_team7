using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudView : MonoBehaviour {
    public static HudView instance;
    public TextMeshProUGUI currentFireText;
    public TextMeshProUGUI currentDistanceText;
    public TextMeshProUGUI gameoverDistanceText;
    public TextMeshProUGUI gameoverFireText;
    public GameObject mainMenu;
    public GameObject gameOverMenu;

    public TextMeshProUGUI mainMenuFireRecord;
    public TextMeshProUGUI mainMenuDistanceRecord;

    public GameObject scorePanel;

    public TextMeshProUGUI recordText;
    public TextMeshProUGUI newRecordText;
    public TextMeshProUGUI recordCounter;

    public GameObject achievementTitle;
    public GameObject achievementDescription;
    public GameObject achievementBackground;

    public int achievementMessageTime = 3;

    private bool achvmtMsgActive = false;

    private Queue<Achievement> achvmtsToShow;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        ReloadHandler.instance.onClickPlay += HideMenus;
        ReloadHandler.instance.onClickRetry += HideMenus;
        PlayerMovementHandler.instance.onPlayerDie += ShowEnding;
        mainMenuFireRecord.text = DataHandler.instance.fireRecord.ToString();
        mainMenuDistanceRecord.text = DataHandler.instance.distanceRecord.ToString();
        achvmtsToShow = new Queue<Achievement>();
    }

    private void HideMenus() {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scorePanel.SetActive(true);
    }

    void ShowEnding() {
        int currentFire = Convert.ToInt32(currentFireText.text);
        DataHandler.instance.IncreaseCash(currentFire);
        DataHandler.instance.SaveFireRecord(currentFire);
        int currentDistance = Convert.ToInt32(currentDistanceText.text);
        bool newHighScore = DataHandler.instance.SaveDistanceRecord(currentDistance);
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
        gameoverFireText.text = currentFireText.text;
        gameoverDistanceText.text = currentDistanceText.text;
        recordCounter.text = DataHandler.instance.distanceRecord.ToString();
        scorePanel.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ShowAchievement(Achievement achievement) {
        achvmtsToShow.Enqueue(achievement);
    }

    public void Update() {
        if (!achvmtMsgActive) {
            if (achvmtsToShow.Count > 0) {
                Achievement ach = achvmtsToShow.Dequeue();
                achvmtMsgActive = true;
                StartCoroutine(ShowAchievementMessage(ach));
            }
        }
    }

    IEnumerator ShowAchievementMessage(Achievement achievement) {
        AudioHandler.instance.Play("Bell");
        achievementTitle.GetComponent<TextMeshProUGUI>().text = LocalizationSystem.GetLocalizedValue(achievement.achievementTitle);
        achievementTitle.SetActive(true);
        achievementDescription.GetComponent<TextMeshProUGUI>().text = LocalizationSystem.GetLocalizedValue(achievement.achievmentDescription);
        achievementDescription.SetActive(true);
        achievementBackground.SetActive(true);
        yield return new WaitForSeconds(achievementMessageTime);
        achievementTitle.SetActive(false);
        achievementDescription.SetActive(false);
        achievementBackground.SetActive(false);
        achvmtMsgActive = false;
    }

    private void OnDestroy() {
        ReloadHandler.instance.onClickPlay -= HideMenus;
        ReloadHandler.instance.onClickRetry -= HideMenus;
        PlayerMovementHandler.instance.onPlayerDie -= ShowEnding;
    }

    public void UpdateFire(int currentScore) {
        currentFireText.text = currentScore.ToString();
    }

    public void UpdateDistance(int currentDistance) {
        currentDistanceText.text = currentDistance.ToString();
    }
}
