using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class HudController : MonoBehaviour {

    public static HudController instance;
    HudView view;
    int currentFire = 0;

    public Action<int> onIncreaseFire;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        view = HudView.instance;
        view.UpdateFire(currentFire);
        PlayerParticleHandler.instance.onFireExtinct += IncreaseFire;
        PlayerDistanceController.instance.onIncreaseDistance += IncreaseDistance;
        ReloadHandler.instance.onClickRetry += ResetCounters;
        AchievementsController inst = AchievementsController.instance;
        inst.onReachAchievement += OnReachAchievement;
    }

    public void IncreaseFire() {
        currentFire++;
        view.UpdateFire(currentFire);
        onIncreaseFire?.Invoke(currentFire);
    }

    void OnReachAchievement(Achievement achievement) {
        Debug.Log("Received achievement reached event");
        view.ShowAchievement(achievement);
    }

    public void IncreaseDistance(int currentDistance) {
        currentDistance++;
        view.UpdateDistance(currentDistance);
    }

    public void ResetCounters() {
        currentFire = 0;
        view.UpdateFire(currentFire);
    }

    private void OnDestroy() {
        PlayerParticleHandler.instance.onFireExtinct -= IncreaseFire;
        PlayerDistanceController.instance.onIncreaseDistance -= IncreaseDistance;
        ReloadHandler.instance.onClickRetry -= ResetCounters;
        AchievementsController.instance.onReachAchievement -= OnReachAchievement;

    }
}
