using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementsController : MonoBehaviour {
    public static AchievementsController instance;

    DataHandler dataHandler;
    List<FireExtinctAchievement> fireAchievements;
    int fireAchievementIndex;

    List<DistanceAchievement> distanceAchievements;
    int distanceAchievementIndex;

    public Action<Achievement> onReachAchievement;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        dataHandler = DataHandler.instance;
        PlayerDistanceController.instance.onIncreaseDistance += CheckDistanceAchievements;
        HudController.instance.onIncreaseFire += CheckFireAchievements;
        dataHandler.onResetHighScores += ResetAchievementIndexes;
        LoadAchievements();
    }

    private void LoadAchievements() {
        fireAchievements = new List<FireExtinctAchievement>();
        distanceAchievements = new List<DistanceAchievement>();
        ICollection keys = LocalizationSystem.GetKeys();
        foreach (string key in keys) {
            if (key.StartsWith("_ach_t_f-")) { //fire achievements
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

            if (key.StartsWith("_ach_t_d-")) { //distance achievements
                string[] countText = key.Split('-')[1].Split('_');
                int count = int.Parse(countText[0]);
                int index = int.Parse(countText[1]);
                string title = "_ach_t_d-" + count + "_" + index;
                string description = "_ach_d_d-" + count + "_" + index; ;
                DistanceAchievement achievement = new DistanceAchievement();
                achievement.achievementTitle = title;
                achievement.achievmentDescription = description;
                achievement.targetCount = count;
                distanceAchievements.Add(achievement);
            }
        }
        fireAchievementIndex = dataHandler.fireAchievementIndex;
        distanceAchievementIndex = dataHandler.distanceAchievementIndex;
        //Debug.Log("Achievements loaded");
    }

    void ResetAchievementIndexes() {
        //Debug.Log("Reseting achievements");
        fireAchievementIndex = 0;
        distanceAchievementIndex = 0;
    }

    private void CheckFireAchievements(int currentFire) {
        //Debug.Log("fireachindex:" + fireAchievementIndex);
        FireExtinctAchievement achievement = fireAchievements[fireAchievementIndex];
        bool achievementReached = currentFire == achievement.targetCount;
        //Debug.Log("[" + fireAchievementIndex + "]Checking fire achievement " + currentFire + " / " + achievement.targetCount);
        if (achievementReached) {
            onReachAchievement?.Invoke(achievement);
            if (fireAchievementIndex < fireAchievements.Count - 1) {
                fireAchievementIndex++;
                dataHandler.SetFireAchievementIndex(fireAchievementIndex);
            }
        }
    }

    private void CheckDistanceAchievements(int currentDistance) {
        //Debug.Log("DistAchCount:" + distanceAchievements.Count + " index:" + distanceAchievementIndex);
        DistanceAchievement achievement = distanceAchievements[distanceAchievementIndex];
        bool achievementReached = currentDistance == achievement.targetCount;
        // Debug.Log("[" + distanceAchievementIndex + "]Checking distance achievement " + currentDistance + " / " + achievement.targetCount);
        if (achievementReached) {
            onReachAchievement?.Invoke(achievement);
            if (distanceAchievementIndex < distanceAchievements.Count - 1) {
                distanceAchievementIndex++;
                dataHandler.SetDistanceAchievementIndex(distanceAchievementIndex);
            }
        }
    }

    private void OnDestroy() {
        PlayerDistanceController.instance.onIncreaseDistance -= CheckDistanceAchievements;
        HudController.instance.onIncreaseFire -= CheckFireAchievements;
        dataHandler.onResetHighScores -= ResetAchievementIndexes;
    }
}
