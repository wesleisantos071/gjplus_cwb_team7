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

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Tree")) {
            onHitTree?.Invoke();
        } else if (other.gameObject.CompareTag("Fire")) {
            onHitFire?.Invoke();
        } else if (other.gameObject.CompareTag("Water")) {
            Destroy(other.transform.parent.gameObject);
            onHitWater?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            onHitFloor?.Invoke();
        }
    }
}
