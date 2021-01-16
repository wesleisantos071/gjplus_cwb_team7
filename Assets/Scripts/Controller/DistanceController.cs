using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour {
    public static DistanceController instance;
    public Action onReachDistance;
    public float distanceToMonitor;
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
            if (transform.position.z - lastMark > distanceToMonitor) {
                onReachDistance?.Invoke();
                lastMark = transform.position.z;
            }
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onPlayerStartMove -= StartMonitor;
    }
}
