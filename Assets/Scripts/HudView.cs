using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudView : MonoBehaviour {
    public static HudView instance;
    public TextMeshProUGUI currentScoreText;
    public GameObject scoreIcon;
    public GameObject mainMenu;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        ReloadHandler.instance.onClickPlay += HideMainMenu;
    }

    private void HideMainMenu() {
        mainMenu.SetActive(false);
    }

    private void OnDestroy() {
        ReloadHandler.instance.onClickPlay -= HideMainMenu;
    }

    public void UpdateCash(int currentScore) {
        currentScoreText.text = currentScore.ToString();
        scoreIcon.GetComponent<Animator>().SetTrigger("increase");
    }
}
