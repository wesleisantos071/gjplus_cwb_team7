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
        Vector3 newScale = transform.localScale;
        newScale.z = 0.15f;
        transform.localScale = newScale;
    }

    private void OnHitFire() {
        GetComponent<MeshRenderer>().material = ashMaterial;
    }

    private void OnDestroy() {
        PlayerCollisionHandler.instance.onHitTree -= OnHitTree;
        PlayerCollisionHandler.instance.onHitFire -= OnHitFire;
    }

}
