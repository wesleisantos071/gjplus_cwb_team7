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
    public float smoothSpeed = .3f;
    public float jumpSpeed = 6;

    direction currentDirection = direction.NONE;

    enum direction {
        NONE,
        TO_LEFT,
        RIGHT_TO_MIDDLE,
        LEFT_TO_MIDDLE,
        TO_RIGHT
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * moveSpeed;
    }

    void Update() {
        int h = HandleInput();
        if (h != 0) {
            SetDestination(h);
        }
        if (currentDirection != direction.NONE) {
            Move();
        }
    }

    private void Move() {
        Vector3 destinationPos = transform.position;
        if (currentDirection == direction.TO_LEFT) {
            if (transform.position.x > leftX) {
                destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            } else {
                destinationPos.x = leftX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.RIGHT_TO_MIDDLE) {
            if (transform.position.x > middleX) {
                destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            } else {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.LEFT_TO_MIDDLE) {
            if (transform.position.x < middleX) {
                destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            } else {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.TO_RIGHT) {
            if (transform.position.x < rightX) {
                destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            } else {
                destinationPos.x = rightX;
                currentDirection = direction.NONE;
            }
        }
        transform.position = destinationPos;
    }

    private void SetDestination(float h) {
        Vector3 currentPos = transform.position;
        if (h > 0) {// move right
            if (currentPos.x < middleX) {
                currentDirection = direction.LEFT_TO_MIDDLE;
            } else if (currentPos.x < rightX) {
                currentDirection = direction.TO_RIGHT;
            }
        } else if (h < 0) {// move left
            if (currentPos.x > middleX) {
                currentDirection = direction.RIGHT_TO_MIDDLE;
            } else if (currentPos.x > leftX) {
                currentDirection = direction.TO_LEFT;
            }
        }
    }

    int HandleInput() {
        int h = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            h = 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            h = -1;
        }
        //TODO: implement swipe 
        return h;
    }
}
