using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour {

    HudView view;
    int currentCash = 0;
    DataHandler dataHandler;


    void Start() {
        view = HudView.instance;
        dataHandler = DataHandler.instance;
        currentCash = dataHandler.playerCash;
        view.UpdateCash(currentCash);
        PlayerParticleHandler.instance.onFireExtinct += IncreaseCash;
    }

    public void IncreaseCash() {
        dataHandler.IncreaseCash(1); ;
        currentCash = dataHandler.playerCash;
        view.UpdateCash(currentCash);
    }

    private void OnDestroy() {
        PlayerParticleHandler.instance.onFireExtinct -= IncreaseCash;
    }
}
