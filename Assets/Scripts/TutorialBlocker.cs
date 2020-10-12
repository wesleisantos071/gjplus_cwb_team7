using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlocker : MonoBehaviour {
    public GameObject hint;
    public direction awaitingDiretionInput;
    public enum direction {
        Right,
        Left,
        Tap
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("Player")) {
            hint.SetActive(true);
            switch (awaitingDiretionInput) {
                case direction.Right:
                    hint.SetActive(true);
                    PlayerMovementHandler.instance.onMoveRight += OnMoveRight;
                    break;
                case direction.Left:
                    hint.SetActive(true);
                    PlayerMovementHandler.instance.onMoveLeft += OnMoveLeft;
                    break;
                case direction.Tap:
                    hint.SetActive(true);
                    PlayerMovementHandler.instance.onJump += OnJump;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMoveRight() {
        hint.SetActive(false);
        PlayerMovementHandler.instance.onMoveRight -= OnMoveRight;
    }

    private void OnMoveLeft() {
        hint.SetActive(false);
        PlayerMovementHandler.instance.onMoveLeft -= OnMoveLeft;
    }

    private void OnJump() {
        hint.SetActive(false);
        PlayerMovementHandler.instance.onJump -= OnJump;
    }
}
