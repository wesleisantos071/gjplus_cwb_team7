using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour {

    HudView view;
    int currentCash = 0;
    DataHandler dataHandler;
    GameObject achievementTitle;
    GameObject achievementDescription;

    List<FireExtinctAchievement> fireAchievements;
    int fireAchievementIndex;

    void Start() {
        view = HudView.instance;
        dataHandler = DataHandler.instance;
        view.UpdateCash(currentCash);
        PlayerParticleHandler.instance.onFireExtinct += IncreaseCash;
        ReloadHandler.instance.onClickPlay += ResetCash;
        LoadAchievements();
    }

    public void IncreaseCash() {
        currentCash++;
        view.UpdateCash(currentCash);
        CheckAchievements();
    }

    private void LoadAchievements() {
        fireAchievements = new List<FireExtinctAchievement>();
        fireAchievementIndex = dataHandler.fireAchievementIndex;
        ICollection keys = LocalizationSystem.GetKeys();
        foreach (string key in keys) {
            if (key.StartsWith("_ach_t_f-")) {
                string[] countText = key.Split('-')[1].Split('_');
                int count = int.Parse(countText[0]);
                int index = int.Parse(countText[1]);
                string title = "_ach_t_f-" + count + "_" + index;
                string description = "_ach_d_f-" + count + "_" + index; ;
                FireExtinctAchievement achievement = new FireExtinctAchievement();
                achievement.achievementTitle = title;
                achievement.achievmentDescription = description;
                achievement.targetCount = count;
                fireAchievements.Add(achievement);
            }
        }
        Debug.Log("Achievement in place: " + fireAchievements[fireAchievementIndex].targetCount);
    }

    private void CheckAchievements() {
        FireExtinctAchievement achievement = fireAchievements[fireAchievementIndex];
        bool achievementReached = currentCash == achievement.targetCount;
        if (achievementReached) {
            view.ShowAchievement(achievement);
            if (fireAchievementIndex < fireAchievements.Count - 1) {
                fireAchievementIndex++;
                dataHandler.SetFireAchievementIndex(fireAchievementIndex);
            }
        }
    }

    public void ResetCash() {
        currentCash = 0;
        view.UpdateCash(currentCash);
    }

    private void OnDestroy() {
        PlayerParticleHandler.instance.onFireExtinct -= IncreaseCash;
        ReloadHandler.instance.onClickPlay -= ResetCash;
    }
}
