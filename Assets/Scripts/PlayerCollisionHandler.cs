using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {
    public static PlayerCollisionHandler instance;

    public Action onHitTree;
    public Action onHitFire;
    public Action onHitFloor;
    public Action onHitWater;
    public Action<GameObject> onHitLane;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Tree") ||
            other.gameObject.CompareTag("Rock")) {
            onHitTree?.Invoke();
        } else if (other.CompareTag("Fire")) {
            onHitFire?.Invoke();
        } else if (other.CompareTag("Water")) {
            Destroy(other.transform.parent.gameObject);
            onHitWater?.Invoke();
        } else if (other.CompareTag("Lane")) {
            onHitLane?.Invoke(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            onHitFloor?.Invoke();
        }
    }
}
