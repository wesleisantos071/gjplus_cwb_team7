using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour {
    public static PlayerParticleHandler instance;
    public ParticleSystem[] parts;

    public GameObject smoke;
    public Action onFireExtinct;

    public LayerMask fireMask;
    public float raycastDistance;
    public Transform raycastOrigin;
    RaycastHit hit;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        PlayerMovementHandler.instance.onJump += StartWaterParticle;
    }
    bool raycastEnabled = false;

    private void StartWaterParticle() {
        foreach (ParticleSystem part in parts) {
            part.Play();
        }
        raycastEnabled = true;
        PlayerCollisionHandler.instance.onHitFloor += StopWaterParticle;
    }

    private void StopWaterParticle() {
        PlayerCollisionHandler.instance.onHitFloor -= StopWaterParticle;
        raycastEnabled = false;
        foreach (ParticleSystem part in parts) {
            part.Stop();
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onJump -= StartWaterParticle;
    }

    private void FixedUpdate() {
        if (raycastEnabled && Physics.Raycast(raycastOrigin.position,
            raycastOrigin.up * -1, out hit, raycastDistance, fireMask, QueryTriggerInteraction.Collide)) {
            hit.collider.GetComponentInChildren<ParticleSystem>().Stop();
            hit.collider.GetComponentInChildren<BoxCollider>().enabled = false;
            onFireExtinct?.Invoke();
            //GameObject go = GameObject.Instantiate(smoke);
            //go.transform.parent = null;
            //go.transform.position = hit.collider.gameObject.transform.position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastOrigin.position, raycastOrigin.up * -raycastDistance);
    }

}
