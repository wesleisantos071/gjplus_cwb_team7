using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour {
    public static PlayerMovementHandler instance;
    Rigidbody rb;
    public float moveSpeed = 5;
    public float leftX = -0.5f;
    public float middleX = 0;
    public float rightX = 0.5f;
    public float smoothSpeed = .3f;
    public float jumpSpeed = 6;
    bool canMove = true;
    direction currentDirection = direction.NONE;

    public Action onJump;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    enum direction {
        NONE,
        TO_LEFT,
        RIGHT_TO_MIDDLE,
        LEFT_TO_MIDDLE,
        TO_RIGHT
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        PlayerCollisionHandler.instance.onHitTree += StopMovement;
        PlayerCollisionHandler.instance.onHitFire += StopMovement;
    }

    private void StopMovement() {
        canMove = false;
    }

    private void OnDestroy() {
        PlayerCollisionHandler.instance.onHitTree -= StopMovement;
        PlayerCollisionHandler.instance.onHitFire -= StopMovement;
    }

    void Update() {
        if (canMove) {
            transform.position = new Vector3(transform.position.x,
                transform.position.y,
                transform.position.z + (moveSpeed * Time.deltaTime));
            Vector2 input = HandleInput();
            if (input.x != 0) {
                SetDestination(input.x);
            }
            if (input.y > 0) {
                Jump();
            }
            if (currentDirection != direction.NONE) {
                MoveHorizontal();
            }
        }
    }

    private void Jump() {
        Vector3 currentVelocity = rb.velocity;
        currentVelocity.y = jumpSpeed / 2.5f;
        rb.velocity = currentVelocity;
        onJump?.Invoke();
    }

    private void MoveHorizontal() {
        Vector3 destinationPos = transform.position;
        if (currentDirection == direction.TO_LEFT) {
            destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            if (transform.position.x < leftX) {
                destinationPos.x = leftX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.RIGHT_TO_MIDDLE) {
            destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            if (transform.position.x < middleX) {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.LEFT_TO_MIDDLE) {
            destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            if (transform.position.x > middleX) {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.TO_RIGHT) {
            destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            if (transform.position.x > rightX) {
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

    Vector2 HandleInput() {
        Vector2 input = new Vector2();
        float h = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            h = 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            h = -1;
        }
        float v = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            v = 1;
        }
        //TODO: implement swipe 
        input.x = h;
        input.y = v;
        return input;
    }
}
