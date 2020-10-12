using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadHandler : MonoBehaviour {

    public static ReloadHandler instance;
    public Action onClickPlay;
    public GameObject loadingScreen;
    public GameObject gameoverScreen;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void OnClickPlay() {
        AudioManager.instance.StopWaterJet();
        onClickPlay?.Invoke();
    }

    public void ReloadSandbox() {
        //fazer isto quando apertar retry
        gameoverScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
