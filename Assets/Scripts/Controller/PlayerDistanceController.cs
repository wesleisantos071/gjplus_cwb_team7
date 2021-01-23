using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceController : MonoBehaviour {
    public static PlayerDistanceController instance;
    public Action onReachDistance;
    public Action<int> onIncreaseDistance;
    private float distanceToMonitor;
    private float lastMark;
    private float totalDistance;
    private int lastRoundedTotal;
    private bool canMonitor = false;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        lastMark = 0;
        lastRoundedTotal = 0;
    }

    private void Start() {
        PlayerMovementHandler.instance.onPlayerStartMove += StartMonitor;
        PlayerMovementHandler.instance.onPlayerDie += StopMonitor;
        distanceToMonitor = PlatformController.instance.platformSize;
    }

    void StopMonitor() {
        canMonitor = false;
    }


    private void StartMonitor() {
        lastMark = 0;
        lastRoundedTotal = 0;
        canMonitor = true;
    }

    void Update() {
        if (canMonitor) {
            totalDistance = transform.position.z;
            if (totalDistance - lastMark > distanceToMonitor) {
                onReachDistance?.Invoke();
                lastMark = totalDistance;
            }
            if (lastRoundedTotal < Convert.ToInt32(totalDistance)) {
                lastRoundedTotal = Convert.ToInt32(totalDistance);
                onIncreaseDistance?.Invoke(lastRoundedTotal);
            }
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onPlayerStartMove -= StartMonitor;
        PlayerMovementHandler.instance.onPlayerDie -= StopMonitor;
    }
}
