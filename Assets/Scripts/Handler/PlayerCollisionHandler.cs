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
        //Debug.Log("Colided with:" + other.name);
        if (other.gameObject.CompareTag("Tree") ||
            other.gameObject.CompareTag("Rock")) {
            //Debug.Log("Player letal collision");
            onHitTree?.Invoke();
        } else if (other.CompareTag("Fire")) {
            //Debug.Log("Player letal collision");
            onHitFire?.Invoke();
        } else if (other.CompareTag("Water")) {
            other.GetComponent<IDestructable>().SimulateDestruction();
            onHitWater?.Invoke();
        } else if (other.CompareTag("Lane")) {
            onHitLane?.Invoke(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.y < 0 && other.gameObject.CompareTag("Floor")) {
            onHitFloor?.Invoke();
        }
    }
}
