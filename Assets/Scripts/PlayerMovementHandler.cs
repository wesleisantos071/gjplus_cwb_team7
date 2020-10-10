using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour {
    Rigidbody rb;
    public float moveSpeed = 5;
    public float leftX = -0.5f;
    public float middleX = 0;
    public float rightX = 0.5f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * moveSpeed;
    }
    void Update() {
        float h = HandleInput();
        Move(h);
    }

    private void Move(float h) {
        Vector3 currentPos = transform.position;
        if (h > 0) {// move right
            if (currentPos.x < middleX) {
                currentPos.x = middleX;
                transform.position = currentPos;
                return;
            }
            if (currentPos.x < rightX) {
                currentPos.x = rightX;
                transform.position = currentPos;
                return;
            }
        }
        if (h < 0) {// move left
            if (currentPos.x > middleX) {
                currentPos.x = middleX;
                transform.position = currentPos;
                return;
            }
            if (currentPos.x > leftX) {
                currentPos.x = leftX;
                transform.position = currentPos;
                return;
            }
        }

    }

    float HandleInput() {
        float h = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            h = 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            h = -1;
        }
        //TODO: implement swipe 
        return h;
    }
}
