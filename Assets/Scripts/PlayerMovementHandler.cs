using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour {
    public static PlayerMovementHandler instance;
    Rigidbody rb;
    public float moveSpeed = 5;
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
        TO_RIGHT
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        PlayerCollisionHandler.instance.onHitTree += StopMovement;
        PlayerCollisionHandler.instance.onHitFire += StopMovement;
        PlayerCollisionHandler.instance.onHitWater += IncreaseWaterLevel;
        PlayerCollisionHandler.instance.onHitLane += PositionInLane;
    }

    private void PositionInLane(GameObject lane) {
        Vector3 position = transform.position;
        position.x = lane.transform.position.x;
        transform.position = position;
        currentDirection = direction.NONE;
    }

    private void IncreaseWaterLevel() {
        AudioManager.instance.Play("WaterSuck");
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
        AudioManager.instance.StopWaterJet();
    }

    private void OnDestroy() {
        PlayerCollisionHandler.instance.onHitTree -= StopMovement;
        PlayerCollisionHandler.instance.onHitFire -= StopMovement;
        PlayerCollisionHandler.instance.onHitWater -= IncreaseWaterLevel;
        PlayerCollisionHandler.instance.onHitLane -= PositionInLane;
    }

    void Update() {
        if (canMove) {
            transform.position = new Vector3(transform.position.x,
                transform.position.y,
                transform.position.z + (moveSpeed * Time.deltaTime));
            Vector2 input = HandleInput();
            if (input.x != 0) {
                SetDirection(input.x);
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
            currentVelocity.y = jumpSpeed / 2;
            rb.velocity = currentVelocity;
            onJump?.Invoke();
        }
    }
    private void MoveHorizontal() {
        Vector3 destinationPos = transform.position;
        if (currentDirection == direction.TO_LEFT) {
            destinationPos.x = destinationPos.x - (jumpSpeed * Time.deltaTime);
        } else if (currentDirection == direction.TO_RIGHT) {
            destinationPos.x = destinationPos.x + (jumpSpeed * Time.deltaTime);
        }
        transform.position = destinationPos;
    }

    private void SetDirection(float h) {
        if (h > 0) {
            currentDirection = direction.TO_RIGHT;
        } else if (h < 0) {
            currentDirection = direction.TO_LEFT;
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
