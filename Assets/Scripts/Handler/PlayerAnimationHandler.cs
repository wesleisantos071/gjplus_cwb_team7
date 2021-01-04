using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour {
    public Material ashMaterial;

    void Start() {
        PlayerCollisionHandler.instance.onHitTree += OnHitTree;
        PlayerCollisionHandler.instance.onHitFire += OnHitFire;
    }

    private void OnHitTree() {
        GameObject[] playerParts = GameObject.FindGameObjectsWithTag("PlayerPart");
        foreach (GameObject playerPart in playerParts) {
            if (playerPart.GetComponent<Rigidbody>() == null) {
                playerPart.AddComponent<Rigidbody>();
                playerPart.AddComponent<CapsuleCollider>();
            }
        }
    }

    private void OnHitFire() {
        GetComponent<MeshRenderer>().material = ashMaterial;
    }

    private void OnDestroy() {
        PlayerCollisionHandler.instance.onHitTree -= OnHitTree;
        PlayerCollisionHandler.instance.onHitFire -= OnHitFire;
    }

}
