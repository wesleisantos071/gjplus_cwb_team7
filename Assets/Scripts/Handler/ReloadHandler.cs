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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 2.03f, 2.03f);
        AudioHandler.instance.StopWaterJet();
        onClickPlay?.Invoke();
    }

    public void ReloadSandbox() {
        //fazer isto quando apertar retry
        gameoverScreen.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
