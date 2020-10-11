using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadHandler : MonoBehaviour {

    public static ReloadHandler instance;
    public Action onClickPlay;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void OnClick() {
        SceneManager.LoadScene(1);
    }

    public void ReloadSandbox() {
        AudioManager.instance.StopWaterJet();
        //SceneManager.LoadScene(0);
        onClickPlay?.Invoke();
    }
}
