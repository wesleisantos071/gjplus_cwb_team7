using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {
    public static PlayerCollisionHandler instance;

    public Action onHitTree;
    public Action onHitFire;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Tree")) {
            onHitTree?.Invoke();
        } else if (other.gameObject.CompareTag("Fire")) {
            onHitFire?.Invoke();
        }
    }
}
