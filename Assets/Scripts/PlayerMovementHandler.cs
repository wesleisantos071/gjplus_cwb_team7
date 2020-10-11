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

    public GameObject[] waterLevels = new GameObject[10];//remaining jumps

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
        PlayerCollisionHandler.instance.onHitWater += IncreaseWaterLevel;
    }

    private void IncreaseWaterLevel() {
        for (int i = 0; i < waterLevels.Length; i++) {
            GameObject water = waterLevels[i];
            if (!water.activeSelf) {
                water.SetActive(true);
                break;
            }
        }
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
        bool hasWater = false;
        for (int i = waterLevels.Length - 1; i >= 0; i--) {
            GameObject water = waterLevels[i];
            if (water.activeSelf) {
                hasWater = true;
                water.SetActive(false);
                break;
            }
        }
        if (hasWater) {
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y = jumpSpeed;
            rb.velocity = currentVelocity;
            onJump?.Invoke();
        }
    }
    public float threshold = 0.05f;
    private void MoveHorizontal() {
        Vector3 destinationPos = transform.position;
        if (currentDirection == direction.TO_LEFT) {
            destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            if (destinationPos.x < leftX) {
                destinationPos.x = leftX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.RIGHT_TO_MIDDLE) {
            destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
            if (destinationPos.x < middleX + threshold) {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.LEFT_TO_MIDDLE) {
            destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            if (destinationPos.x > middleX - threshold) {
                destinationPos.x = middleX;
                currentDirection = direction.NONE;
            }
        } else if (currentDirection == direction.TO_RIGHT) {
            destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
            if (destinationPos.x > rightX) {
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
        float h = HandleSwipe();
        if (h == 0) {
            h = HandleKeyboard();
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

    private int HandleKeyboard() {
        int h = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            h = 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            h = -1;
        }
        return h;
    }

    private int HandleSwipe() {
        int h = 0;
        if (swipeStartMarker == Vector3.zero) {
            HandleInputIn();
        } else {
            h = HandleInputOut();
        }
        return h;
    }

    protected Vector3 swipeStartMarker = Vector3.zero;
    protected Vector3 swipeEndMarker = Vector3.zero;
    public float minDistance = 20f;
    private void HandleInputIn() {
        if (Input.GetMouseButtonDown(0)) {
            swipeStartMarker = Input.mousePosition;
        }
    }

    private int HandleInputOut() {
        int h = 0;
        if (Input.GetMouseButtonUp(0)) {
            swipeEndMarker = Input.mousePosition;
            h = GetDirection();
            swipeEndMarker = Vector3.zero;
            swipeStartMarker = Vector3.zero;
        }
        return h;
    }

    private int GetDirection() {
        float distX = swipeEndMarker.x - swipeStartMarker.x;
        int horizontal = 0;
        if (Math.Abs(distX) > minDistance) {
            if (distX > 0) {
                horizontal = 1;
            } else {
                horizontal = -1;
            }
        } else {
            Jump();
        }
        return horizontal;
    }
}
