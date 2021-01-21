using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceController : MonoBehaviour {
    public static PlayerDistanceController instance;
    public Action onReachDistance;
    private float distanceToMonitor;
    private float lastMark;
    private float totalDistance;
    private bool canMonitor = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        lastMark = 0;
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
        canMonitor = true;
    }

    void Update() {
        if (canMonitor) {
            totalDistance = transform.position.z;
            if (totalDistance - lastMark > distanceToMonitor) {
                onReachDistance?.Invoke();
                lastMark = totalDistance;
            }
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onPlayerStartMove -= StartMonitor;
        PlayerMovementHandler.instance.onPlayerDie -= StopMonitor;
    }
}
