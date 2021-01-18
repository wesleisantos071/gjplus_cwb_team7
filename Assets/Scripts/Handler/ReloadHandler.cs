using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadHandler : MonoBehaviour {

    public static ReloadHandler instance;
    public Action onClickPlay;
    public Action onClickRetry;
    public GameObject loadingScreen;
    public GameObject gameoverScreen;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void OnClickPlay() {
        AudioHandler.instance.StopWaterJet();
        onClickPlay?.Invoke();
    }

    public void OnClickRetry() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 2.03f, 0);
        AudioHandler.instance.StopWaterJet();
        onClickRetry?.Invoke();
    }

    public void ReloadSandbox() {
        gameoverScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
